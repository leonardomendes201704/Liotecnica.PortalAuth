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

[Authorize(Policy = PermissionCodes.PermissionManage)]
public sealed class PermissionsController : Controller
{
    private readonly PortalAuthDbContext _dbContext;
    private readonly IAuditService _auditService;

    public PermissionsController(PortalAuthDbContext dbContext, IAuditService auditService)
    {
        _dbContext = dbContext;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var permissions = await _dbContext.Permissions
            .AsNoTracking()
            .Where(permission => !permission.IsDeleted)
            .OrderBy(permission => permission.Module)
            .ThenBy(permission => permission.Code)
            .ToListAsync();

        return View(permissions);
    }

    public IActionResult Create()
    {
        return View(new PermissionFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PermissionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var permission = new Permission(model.Code, model.Description, model.Module, model.IsCritical);
        permission.Update(model.Code, model.Description, model.Module, model.IsCritical, model.IsActive, User.Identity?.Name);

        _dbContext.Permissions.Add(permission);
        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Created,
            nameof(Permission),
            permission.Id.ToString(),
            $"Permissao criada: {permission.Code} no modulo {permission.Module}.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var permission = await _dbContext.Permissions.FindAsync(id);

        if (permission is null)
        {
            return NotFound();
        }

        return View(new PermissionFormViewModel
        {
            Id = permission.Id,
            Code = permission.Code,
            Description = permission.Description,
            Module = permission.Module,
            IsCritical = permission.IsCritical,
            IsActive = permission.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PermissionFormViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var permission = await _dbContext.Permissions.FindAsync(id);

        if (permission is null)
        {
            return NotFound();
        }

        permission.Update(model.Code, model.Description, model.Module, model.IsCritical, model.IsActive, User.Identity?.Name);
        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Updated,
            nameof(Permission),
            permission.Id.ToString(),
            $"Permissao atualizada: {permission.Code} no modulo {permission.Module}. Ativa: {permission.IsActive}.");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var permission = await _dbContext.Permissions.FindAsync(id);

        if (permission is null)
        {
            return NotFound();
        }

        permission.Update(
            permission.Code,
            permission.Description,
            permission.Module,
            permission.IsCritical,
            isActive: false,
            updatedBy: User.Identity?.Name);
        permission.MarkAsDeleted(User.Identity?.Name);

        await _dbContext.SaveChangesAsync();
        await _auditService.RecordAsync(
            AuditAction.Deleted,
            nameof(Permission),
            permission.Id.ToString(),
            $"Permissao excluida logicamente: {permission.Code}.");

        TempData["SuccessMessage"] = $"Permissao {permission.Code} excluida logicamente.";

        return RedirectToAction(nameof(Index));
    }
}
