using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize]
public sealed class StatusController : Controller
{
    private readonly HealthCheckService _healthCheckService;
    private readonly PortalAuthDbContext _dbContext;
    private readonly IAuditService _auditService;

    public StatusController(
        HealthCheckService healthCheckService,
        PortalAuthDbContext dbContext,
        IAuditService auditService)
    {
        _healthCheckService = healthCheckService;
        _dbContext = dbContext;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        var components = report.Entries
            .OrderBy(entry => entry.Key)
            .Select(entry => new OperationalComponentViewModel(
                entry.Key,
                entry.Value.Status.ToString(),
                entry.Value.Description ?? GetDefaultDescription(entry.Key, entry.Value.Status),
                $"{entry.Value.Duration.TotalMilliseconds:N0} ms",
                entry.Value.Tags.OrderBy(tag => tag).ToList()))
            .ToList();
        var checkedAt = DateTime.UtcNow;
        var summary = string.Join("; ", components.Select(component => $"{component.Name}:{component.Status}"));

        _dbContext.OperationalStatusSnapshots.Add(new OperationalStatusSnapshot(
            report.Status.ToString(),
            checkedAt,
            Convert.ToInt64(report.TotalDuration.TotalMilliseconds),
            summary));
        await _dbContext.SaveChangesAsync();

        await _auditService.RecordAsync(
            AuditAction.StatusSnapshotRecorded,
            nameof(OperationalStatusSnapshot),
            null,
            $"Snapshot de status operacional registrado: {report.Status}.");

        var history = await _dbContext.OperationalStatusSnapshots
            .AsNoTracking()
            .OrderByDescending(snapshot => snapshot.CheckedAt)
            .Take(10)
            .Select(snapshot => new OperationalStatusHistoryItemViewModel(
                snapshot.CheckedAt,
                snapshot.Status,
                snapshot.TotalDurationMilliseconds,
                snapshot.ComponentsSummary))
            .ToListAsync();

        var model = new OperationalStatusViewModel
        {
            OverallStatus = report.Status.ToString(),
            CheckedAt = checkedAt,
            TotalDuration = report.TotalDuration,
            Components = components,
            History = history
        };

        return View(model);
    }

    private static string GetDefaultDescription(string name, HealthStatus status)
    {
        return status == HealthStatus.Healthy
            ? $"{name} operacional."
            : $"{name} requer atencao.";
    }
}
