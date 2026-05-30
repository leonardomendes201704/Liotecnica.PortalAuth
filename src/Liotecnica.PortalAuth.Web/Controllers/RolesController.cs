using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Security;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize(Policy = PermissionCodes.RoleManage)]
public sealed class RolesController : Controller
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly PortalAuthDbContext _dbContext;
    private readonly IAuditService _auditService;

    public RolesController(
        RoleManager<IdentityRole<Guid>> roleManager,
        PortalAuthDbContext dbContext,
        IAuditService auditService)
    {
        _roleManager = roleManager;
        _dbContext = dbContext;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles
            .OrderBy(role => role.Name)
            .ToListAsync();

        return View(roles);
    }

    public async Task<IActionResult> Create()
    {
        return View(await BuildRoleFormAsync(new RoleFormViewModel()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildRoleFormAsync(model));
        }

        var role = new IdentityRole<Guid>(model.Name);
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(await BuildRoleFormAsync(model));
        }

        await SyncPermissionsAsync(role.Id, model.SelectedPermissionIds);
        await SyncSystemsAsync(role.Id, model.SelectedSystemIds);
        await _auditService.RecordAsync(
            AuditAction.Created,
            nameof(IdentityRole<Guid>),
            role.Id.ToString(),
            $"Perfil criado: {role.Name}. Permissoes: {model.SelectedPermissionIds.Count}. Sistemas: {model.SelectedSystemIds.Count}.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role is null)
        {
            return NotFound();
        }

        var selectedPermissionIds = await _dbContext.RolePermissions
            .Where(rolePermission => rolePermission.RoleId == id)
            .Select(rolePermission => rolePermission.PermissionId)
            .ToListAsync();

        var selectedSystemIds = await _dbContext.RoleSystemAccesses
            .Where(access => access.RoleId == id)
            .Select(access => access.SystemId)
            .ToListAsync();

        return View(await BuildRoleFormAsync(new RoleFormViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            SelectedPermissionIds = selectedPermissionIds,
            SelectedSystemIds = selectedSystemIds
        }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RoleFormViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildRoleFormAsync(model));
        }

        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role is null)
        {
            return NotFound();
        }

        role.Name = model.Name;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(await BuildRoleFormAsync(model));
        }

        await SyncPermissionsAsync(role.Id, model.SelectedPermissionIds);
        await SyncSystemsAsync(role.Id, model.SelectedSystemIds);
        await _auditService.RecordAsync(
            AuditAction.Updated,
            nameof(IdentityRole<Guid>),
            role.Id.ToString(),
            $"Perfil atualizado: {role.Name}. Permissoes: {model.SelectedPermissionIds.Count}. Sistemas: {model.SelectedSystemIds.Count}.");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role is null)
        {
            return NotFound();
        }

        var hasUsers = await _dbContext.UserRoles.AnyAsync(userRole => userRole.RoleId == id);

        if (hasUsers)
        {
            TempData["ErrorMessage"] = $"Perfil {role.Name} possui usuarios vinculados e nao pode ser removido.";
            return RedirectToAction(nameof(Index));
        }

        var permissions = await _dbContext.RolePermissions
            .Where(rolePermission => rolePermission.RoleId == id)
            .ToListAsync();
        var systems = await _dbContext.RoleSystemAccesses
            .Where(access => access.RoleId == id)
            .ToListAsync();

        _dbContext.RolePermissions.RemoveRange(permissions);
        _dbContext.RoleSystemAccesses.RemoveRange(systems);
        await _dbContext.SaveChangesAsync();

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            TempData["ErrorMessage"] = $"Nao foi possivel remover o perfil {role.Name}.";
            return RedirectToAction(nameof(Index));
        }

        await _auditService.RecordAsync(
            AuditAction.Deleted,
            nameof(IdentityRole<Guid>),
            role.Id.ToString(),
            $"Perfil removido sem usuarios vinculados: {role.Name}.");

        TempData["SuccessMessage"] = $"Perfil {role.Name} removido.";

        return RedirectToAction(nameof(Index));
    }

    private async Task<RoleFormViewModel> BuildRoleFormAsync(RoleFormViewModel model)
    {
        var permissions = await _dbContext.Permissions
            .AsNoTracking()
            .Where(permission => permission.IsActive && !permission.IsDeleted)
            .OrderBy(permission => permission.Module)
            .ThenBy(permission => permission.Code)
            .ToListAsync();

        model.Permissions = permissions
            .Select(permission => new SelectListItem(
                $"{permission.Module} - {permission.Code}",
                permission.Id.ToString(),
                model.SelectedPermissionIds.Contains(permission.Id)))
            .ToList();

        var systems = await _dbContext.CorporateSystems
            .AsNoTracking()
            .Where(system => system.IsActive && !system.IsDeleted)
            .OrderBy(system => system.Name)
            .ToListAsync();

        model.Systems = systems
            .Select(system => new SelectListItem(
                $"{system.Name} ({system.Code})",
                system.Id.ToString(),
                model.SelectedSystemIds.Contains(system.Id)))
            .ToList();

        return model;
    }

    private async Task SyncPermissionsAsync(Guid roleId, IReadOnlyCollection<Guid> permissionIds)
    {
        var existing = await _dbContext.RolePermissions
            .Where(rolePermission => rolePermission.RoleId == roleId)
            .ToListAsync();

        _dbContext.RolePermissions.RemoveRange(existing);

        foreach (var permissionId in permissionIds.Distinct())
        {
            _dbContext.RolePermissions.Add(new RolePermission(roleId, permissionId));
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task SyncSystemsAsync(Guid roleId, IReadOnlyCollection<Guid> systemIds)
    {
        var existing = await _dbContext.RoleSystemAccesses
            .Where(access => access.RoleId == roleId)
            .ToListAsync();

        _dbContext.RoleSystemAccesses.RemoveRange(existing);

        foreach (var systemId in systemIds.Distinct())
        {
            _dbContext.RoleSystemAccesses.Add(new RoleSystemAccess(roleId, systemId));
        }

        await _dbContext.SaveChangesAsync();
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
