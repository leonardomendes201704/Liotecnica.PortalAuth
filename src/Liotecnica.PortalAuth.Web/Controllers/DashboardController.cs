using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize]
public sealed class DashboardController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PortalAuthDbContext _dbContext;

    public DashboardController(UserManager<ApplicationUser> userManager, PortalAuthDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return Challenge();
        }

        var systems = await (
            from userRole in _dbContext.UserRoles
            join access in _dbContext.RoleSystemAccesses on userRole.RoleId equals access.RoleId
            join system in _dbContext.CorporateSystems on access.SystemId equals system.Id
            where userRole.UserId == userId
                  && system.IsActive
                  && !system.IsDeleted
            select system)
            .Distinct()
            .OrderBy(system => system.Name)
            .ToListAsync();

        var model = new DashboardViewModel
        {
            DisplayName = user?.DisplayName ?? User.Identity?.Name ?? "Usuario",
            Department = user?.Department ?? "Portal Corporativo",
            Systems = systems.Select((system, index) => new SystemCardViewModel(
                system.Name,
                system.Description ?? system.BaseUrl,
                string.IsNullOrWhiteSpace(system.Icon) ? system.Code[..Math.Min(2, system.Code.Length)] : system.Icon,
                GetAccent(index),
                system.BaseUrl)).ToList(),
            Notices =
            [
                new("Atualizacao do Sistema Financeiro", "Hoje, 09:15"),
                new("Manutencao programada no domingo", "Ontem, 14:30"),
                new("Nova politica de seguranca da informacao", "25/05, 11:45")
            ]
        };

        return View(model);
    }

    private static string GetAccent(int index)
    {
        var accents = new[] { "purple", "green", "orange", "blue" };

        return accents[index % accents.Length];
    }
}
