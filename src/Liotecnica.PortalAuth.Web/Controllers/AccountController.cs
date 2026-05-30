using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Liotecnica.PortalAuth.Web.Controllers;

public sealed class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuditService _auditService;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IAuditService auditService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _auditService = auditService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            await _auditService.RecordAsync(
                AuditAction.LoginSucceeded,
                "Authentication",
                user?.Id.ToString(),
                "Login realizado com sucesso.",
                user?.Id.ToString(),
                user?.Email ?? model.Email);

            if (user?.MustChangePassword == true)
            {
                return RedirectToAction(nameof(ChangePassword), new { forced = true });
            }

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        await _auditService.RecordAsync(
            AuditAction.LoginFailed,
            "Authentication",
            null,
            "Tentativa de login com e-mail ou senha invalidos.",
            null,
            model.Email);

        ModelState.AddModelError(string.Empty, "E-mail ou senha invalidos.");

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction(nameof(Login));
        }

        return View(new ProfileViewModel
        {
            DisplayName = user.DisplayName,
            Email = user.Email ?? string.Empty,
            Department = user.Department,
            MustChangePassword = user.MustChangePassword
        });
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword(bool forced = false)
    {
        ViewData["ForcedChange"] = forced;

        return View(new ChangePasswordViewModel());
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction(nameof(Login));
        }

        if (!ModelState.IsValid)
        {
            ViewData["ForcedChange"] = user.MustChangePassword;
            return View(model);
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewData["ForcedChange"] = user.MustChangePassword;
            return View(model);
        }

        user.MustChangePassword = false;
        await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user);

        await _auditService.RecordAsync(
            AuditAction.PasswordChanged,
            nameof(ApplicationUser),
            user.Id.ToString(),
            "Senha alterada pelo proprio usuario.");

        TempData["SuccessMessage"] = "Senha alterada com sucesso.";

        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _auditService.RecordAsync(
            AuditAction.Logout,
            "Authentication",
            null,
            "Logout realizado pelo usuario.");

        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
