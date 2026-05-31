using System.Security.Claims;
using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Liotecnica.PortalAuth.Web.Controllers;

public sealed class ConnectController : Controller
{
    private readonly PortalAuthDbContext _dbContext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConnectController(
        PortalAuthDbContext dbContext,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> AuthorizeEndpoint()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OpenID Connect request nao encontrada.");

        if (User.Identity?.IsAuthenticated != true)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                    Request.HasFormContentType
                        ? Request.Form.Select(parameter => new KeyValuePair<string, string?>(parameter.Key, parameter.Value))
                        : Request.Query.Select(parameter => new KeyValuePair<string, string?>(parameter.Key, parameter.Value)))
            }, IdentityConstants.ApplicationScheme);
        }

        var user = await _userManager.GetUserAsync(User);

        if (user is null || user.LockoutEnd > DateTimeOffset.UtcNow || user.MustChangePassword)
        {
            return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var principal = await CreateClaimsPrincipalAsync(user, request.GetScopes());

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("~/connect/token")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> TokenEndpoint()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OpenID Connect request nao encontrada.");

        if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
        {
            return BadRequest("Grant type nao suportado.");
        }

        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        var userId = result.Principal?.GetClaim(OpenIddictConstants.Claims.Subject);

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var user = await _userManager.FindByIdAsync(parsedUserId.ToString());

        if (user is null || user.LockoutEnd > DateTimeOffset.UtcNow || user.MustChangePassword)
        {
            return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var principal = await CreateClaimsPrincipalAsync(user, result.Principal?.GetScopes() ?? []);

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("~/connect/userinfo")]
    [HttpPost("~/connect/userinfo")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UserInfoEndpoint()
    {
        var userId = User.GetClaim(OpenIddictConstants.Claims.Subject);

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return Challenge(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var user = await _userManager.FindByIdAsync(parsedUserId.ToString());

        if (user is null)
        {
            return Challenge(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var permissions = await GetPermissionsAsync(user.Id);

        return Ok(new Dictionary<string, object?>
        {
            [OpenIddictConstants.Claims.Subject] = user.Id.ToString(),
            [OpenIddictConstants.Claims.Email] = user.Email,
            [OpenIddictConstants.Claims.Name] = user.DisplayName,
            [OpenIddictConstants.Claims.Role] = roles,
            ["department"] = user.Department,
            ["permissions"] = permissions
        });
    }

    [HttpGet("~/connect/logout")]
    public async Task<IActionResult> LogoutEndpoint()
    {
        await _signInManager.SignOutAsync();

        return SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private async Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(ApplicationUser user, IEnumerable<string> scopes)
    {
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;
        var roles = await _userManager.GetRolesAsync(user);
        var permissions = await GetPermissionsAsync(user.Id);

        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id.ToString()));
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, user.DisplayName));

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Email, user.Email));
        }

        if (!string.IsNullOrWhiteSpace(user.Department))
        {
            identity.AddClaim(new Claim("department", user.Department));
        }

        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, role));
        }

        foreach (var permission in permissions)
        {
            identity.AddClaim(new Claim("permission", permission));
        }

        principal.SetScopes(scopes);
        principal.SetResources("openfiis");

        foreach (var claim in principal.Claims)
        {
            claim.SetDestinations(GetDestinations(claim));
        }

        return principal;
    }

    private async Task<IReadOnlyCollection<string>> GetPermissionsAsync(Guid userId)
    {
        return await (
            from userRole in _dbContext.UserRoles
            join role in _dbContext.Roles on userRole.RoleId equals role.Id
            join rolePermission in _dbContext.RolePermissions on userRole.RoleId equals rolePermission.RoleId
            join permission in _dbContext.Permissions on rolePermission.PermissionId equals permission.Id
            where userRole.UserId == userId
                  && role.IsActive
                  && !role.IsDeleted
                  && permission.IsActive
                  && !permission.IsDeleted
            select permission.Code)
            .Distinct()
            .OrderBy(code => code)
            .ToListAsync();
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        return claim.Type switch
        {
            OpenIddictConstants.Claims.Name or
            OpenIddictConstants.Claims.Email or
            OpenIddictConstants.Claims.Role or
            "department" or
            "permission" => [OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken],
            _ => [OpenIddictConstants.Destinations.AccessToken]
        };
    }
}
