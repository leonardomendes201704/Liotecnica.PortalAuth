using Liotecnica.PortalAuth.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize]
public sealed class StatusController : Controller
{
    private readonly HealthCheckService _healthCheckService;

    public StatusController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    public async Task<IActionResult> Index()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        var model = new OperationalStatusViewModel
        {
            OverallStatus = report.Status.ToString(),
            CheckedAt = DateTime.UtcNow,
            TotalDuration = report.TotalDuration,
            Components = report.Entries
                .OrderBy(entry => entry.Key)
                .Select(entry => new OperationalComponentViewModel(
                    entry.Key,
                    entry.Value.Status.ToString(),
                    entry.Value.Description ?? GetDefaultDescription(entry.Key, entry.Value.Status),
                    $"{entry.Value.Duration.TotalMilliseconds:N0} ms",
                    entry.Value.Tags.OrderBy(tag => tag).ToList()))
                .ToList()
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
