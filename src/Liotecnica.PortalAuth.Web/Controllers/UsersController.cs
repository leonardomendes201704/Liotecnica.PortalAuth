using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Security;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize(Policy = PermissionCodes.UserManage)]
public sealed class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IAuditService _auditService;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IAuditService auditService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
            .OrderBy(user => user.DisplayName)
            .ToListAsync();

        return View(users);
    }

    public async Task<IActionResult> Create()
    {
        return View(await BuildCreateModelAsync(new UserCreateViewModel()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildCreateModelAsync(model));
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
            DisplayName = model.DisplayName,
            Department = model.Department
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(await BuildCreateModelAsync(model));
        }

        await SyncRolesAsync(user, model.SelectedRoleIds);
        await _auditService.RecordAsync(
            AuditAction.Created,
            nameof(ApplicationUser),
            user.Id.ToString(),
            $"Usuario criado: {user.Email}. Perfis vinculados: {model.SelectedRoleIds.Count}.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var selectedRoleIds = await _roleManager.Roles
            .Where(role => userRoles.Contains(role.Name!))
            .Select(role => role.Id)
            .ToListAsync();

        return View(await BuildEditModelAsync(new UserEditViewModel
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email ?? string.Empty,
            Department = user.Department,
            IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow,
            SelectedRoleIds = selectedRoleIds
        }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildEditModelAsync(model));
        }

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        user.UserName = model.Email;
        user.Email = model.Email;
        user.DisplayName = model.DisplayName;
        user.Department = model.Department;
        user.LockoutEnd = model.IsLocked ? DateTimeOffset.UtcNow.AddYears(100) : null;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(await BuildEditModelAsync(model));
        }

        await SyncRolesAsync(user, model.SelectedRoleIds);
        await _auditService.RecordAsync(
            AuditAction.Updated,
            nameof(ApplicationUser),
            user.Id.ToString(),
            $"Usuario atualizado: {user.Email}. Bloqueado: {model.IsLocked}. Perfis vinculados: {model.SelectedRoleIds.Count}.");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Nao foi possivel bloquear o usuario.";
            return RedirectToAction(nameof(Index));
        }

        await _auditService.RecordAsync(
            AuditAction.Deactivated,
            nameof(ApplicationUser),
            user.Id.ToString(),
            $"Usuario bloqueado/desativado: {user.Email}.");

        TempData["SuccessMessage"] = $"Usuario {user.Email} bloqueado.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reactivate(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        user.LockoutEnd = null;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Nao foi possivel reativar o usuario.";
            return RedirectToAction(nameof(Index));
        }

        await _auditService.RecordAsync(
            AuditAction.Reactivated,
            nameof(ApplicationUser),
            user.Id.ToString(),
            $"Usuario reativado: {user.Email}.");

        TempData["SuccessMessage"] = $"Usuario {user.Email} reativado.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ResetPassword(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        return View(new UserResetPasswordViewModel
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email ?? string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(Guid id, UserResetPasswordViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound();
        }

        model.DisplayName = user.DisplayName;
        model.Email = user.Email ?? string.Empty;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(model);
        }

        user.MustChangePassword = true;
        await _userManager.UpdateAsync(user);

        await _auditService.RecordAsync(
            AuditAction.PasswordReset,
            nameof(ApplicationUser),
            user.Id.ToString(),
            $"Senha redefinida administrativamente para o usuario: {user.Email}.");

        TempData["SuccessMessage"] = $"Senha de {user.Email} redefinida com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    private async Task<UserCreateViewModel> BuildCreateModelAsync(UserCreateViewModel model)
    {
        model.Roles = await BuildRoleItemsAsync(model.SelectedRoleIds);

        return model;
    }

    private async Task<UserEditViewModel> BuildEditModelAsync(UserEditViewModel model)
    {
        model.Roles = await BuildRoleItemsAsync(model.SelectedRoleIds);

        return model;
    }

    private async Task<List<SelectListItem>> BuildRoleItemsAsync(IReadOnlyCollection<Guid> selectedRoleIds)
    {
        var roles = await _roleManager.Roles
            .Where(role => role.IsActive && !role.IsDeleted)
            .OrderBy(role => role.Name)
            .ToListAsync();

        return roles
            .Select(role => new SelectListItem(role.Name, role.Id.ToString(), selectedRoleIds.Contains(role.Id)))
            .ToList();
    }

    private async Task SyncRolesAsync(ApplicationUser user, IReadOnlyCollection<Guid> selectedRoleIds)
    {
        var currentRoleNames = await _userManager.GetRolesAsync(user);
        var roles = await _roleManager.Roles
            .Where(role => selectedRoleIds.Contains(role.Id))
            .Select(role => role.Name!)
            .ToListAsync();

        await _userManager.RemoveFromRolesAsync(user, currentRoleNames);

        if (roles.Count > 0)
        {
            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
