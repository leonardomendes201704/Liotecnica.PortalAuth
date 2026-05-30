using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Security;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize(Policy = PermissionCodes.SystemView)]
public sealed class SystemsController : Controller
{
    private readonly PortalAuthDbContext _dbContext;
    private readonly IAuditService _auditService;

    public SystemsController(PortalAuthDbContext dbContext, IAuditService auditService)
    {
        _dbContext = dbContext;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var systems = await _dbContext.CorporateSystems
            .AsNoTracking()
            .Where(system => !system.IsDeleted)
            .OrderBy(system => system.Name)
            .ToListAsync();

        return View(systems);
    }

    [Authorize(Policy = PermissionCodes.SystemCreate)]
    public IActionResult Create()
    {
        return View(new CorporateSystemFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = PermissionCodes.SystemCreate)]
    public async Task<IActionResult> Create(CorporateSystemFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var system = new CorporateSystem(
            model.Name,
            model.Code,
            model.Description,
            model.BaseUrl,
            model.Icon,
            model.RequiresMfa);

        system.Update(model.Name, model.Code, model.Description, model.BaseUrl, model.Icon, model.RequiresMfa, model.IsActive, User.Identity?.Name);

        _dbContext.CorporateSystems.Add(system);
        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Created,
            nameof(CorporateSystem),
            system.Id.ToString(),
            $"Sistema corporativo criado: {system.Name} ({system.Code}).");

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = PermissionCodes.SystemEdit)]
    public async Task<IActionResult> Edit(Guid id)
    {
        var system = await _dbContext.CorporateSystems.FindAsync(id);

        if (system is null)
        {
            return NotFound();
        }

        return View(new CorporateSystemFormViewModel
        {
            Id = system.Id,
            Name = system.Name,
            Code = system.Code,
            Description = system.Description,
            BaseUrl = system.BaseUrl,
            Icon = system.Icon,
            IsActive = system.IsActive,
            RequiresMfa = system.RequiresMfa
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = PermissionCodes.SystemEdit)]
    public async Task<IActionResult> Edit(Guid id, CorporateSystemFormViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var system = await _dbContext.CorporateSystems.FindAsync(id);

        if (system is null)
        {
            return NotFound();
        }

        system.Update(model.Name, model.Code, model.Description, model.BaseUrl, model.Icon, model.RequiresMfa, model.IsActive, User.Identity?.Name);
        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Updated,
            nameof(CorporateSystem),
            system.Id.ToString(),
            $"Sistema corporativo atualizado: {system.Name} ({system.Code}). Ativo: {system.IsActive}.");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = PermissionCodes.SystemEdit)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var system = await _dbContext.CorporateSystems.FindAsync(id);

        if (system is null)
        {
            return NotFound();
        }

        system.Update(
            system.Name,
            system.Code,
            system.Description,
            system.BaseUrl,
            system.Icon,
            system.RequiresMfa,
            isActive: false,
            updatedBy: User.Identity?.Name);
        system.MarkAsDeleted(User.Identity?.Name);

        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Deleted,
            nameof(CorporateSystem),
            system.Id.ToString(),
            $"Sistema corporativo excluido logicamente: {system.Name} ({system.Code}).");

        TempData["SuccessMessage"] = $"Sistema {system.Name} excluido logicamente.";

        return RedirectToAction(nameof(Index));
    }
}
