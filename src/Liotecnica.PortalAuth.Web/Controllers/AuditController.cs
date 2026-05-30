using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Models;
using Liotecnica.PortalAuth.Web.Security;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Liotecnica.PortalAuth.Web.Controllers;

[Authorize(Policy = PermissionCodes.AuditView)]
public sealed class AuditController : Controller
{
    private const int CsvExportLimit = 10_000;

    private readonly PortalAuthDbContext _dbContext;
    private readonly IAuditRetentionService _auditRetentionService;
    private readonly IAuditService _auditService;

    public AuditController(
        PortalAuthDbContext dbContext,
        IAuditRetentionService auditRetentionService,
        IAuditService auditService)
    {
        _dbContext = dbContext;
        _auditRetentionService = auditRetentionService;
        _auditService = auditService;
    }

    public async Task<IActionResult> Index(AuditFilterViewModel filters, int page = 1, int pageSize = 25)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 10, 100);

        var query = ApplyFilters(_dbContext.AuditLogs.AsNoTracking(), filters);

        var totalItems = await query.CountAsync();
        var totalPages = totalItems == 0 ? 1 : (int)Math.Ceiling(totalItems / (double)pageSize);
        page = Math.Min(page, totalPages);

        var logs = await query
            .OrderByDescending(log => log.OccurredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(log => new AuditLogItemViewModel(
                log.OccurredAt,
                log.Action.ToString(),
                log.EntityName,
                log.EntityId,
                log.UserName,
                log.IpAddress,
                log.CorrelationId,
                log.Details))
            .ToListAsync();

        var availableActions = Enum.GetNames<AuditAction>()
            .OrderBy(actionName => actionName)
            .ToList();

        var availableEntities = await _dbContext.AuditLogs
            .AsNoTracking()
            .Select(log => log.EntityName)
            .Distinct()
            .OrderBy(entity => entity)
            .ToListAsync();
        var retentionDays = await _auditRetentionService.GetRetentionDaysAsync();

        return View(new AuditIndexViewModel
        {
            Logs = logs,
            Filters = filters,
            AvailableActions = availableActions,
            AvailableEntities = availableEntities,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            Retention = new AuditRetentionViewModel { RetentionDays = retentionDays }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRetention(int retentionDays)
    {
        await _auditRetentionService.SetRetentionDaysAsync(retentionDays, User.Identity?.Name);
        await _auditService.RecordAsync(
            AuditAction.RetentionUpdated,
            "AppSetting",
            AuditRetentionService.RetentionDaysKey,
            $"Politica de retencao de auditoria atualizada para {retentionDays} dias.");

        TempData["SuccessMessage"] = $"Retencao de auditoria atualizada para {retentionDays} dias.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ExportCsv(AuditFilterViewModel filters)
    {
        var logs = await ApplyFilters(_dbContext.AuditLogs.AsNoTracking(), filters)
            .OrderByDescending(log => log.OccurredAt)
            .Take(CsvExportLimit)
            .Select(log => new AuditLogItemViewModel(
                log.OccurredAt,
                log.Action.ToString(),
                log.EntityName,
                log.EntityId,
                log.UserName,
                log.IpAddress,
                log.CorrelationId,
                log.Details))
            .ToListAsync();

        await _auditService.RecordAsync(
            AuditAction.AuditExported,
            "AuditLog",
            null,
            $"Exportacao CSV de auditoria executada. Registros exportados: {logs.Count}. Limite: {CsvExportLimit}. Filtros: {DescribeFilters(filters)}.");

        var csv = BuildCsv(logs);
        var fileName = $"auditoria-{DateTime.UtcNow:yyyyMMdd-HHmmss}.csv";

        return File(Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray(), "text/csv", fileName);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PurgeExpired()
    {
        var result = await _auditRetentionService.PurgeExpiredAsync();
        await _auditService.RecordAsync(
            AuditAction.AuditPurged,
            "AuditLog",
            null,
            $"Limpeza de auditoria executada. Retencao: {result.RetentionDays} dias. Corte UTC: {result.CutoffDate:O}. Registros removidos: {result.DeletedCount}.");

        TempData["SuccessMessage"] = $"{result.DeletedCount} eventos de auditoria removidos ate {result.CutoffDate:dd/MM/yyyy HH:mm:ss} UTC.";

        return RedirectToAction(nameof(Index));
    }

    private static IQueryable<AuditLog> ApplyFilters(
        IQueryable<AuditLog> query,
        AuditFilterViewModel filters)
    {
        if (filters.From.HasValue)
        {
            query = query.Where(log => log.OccurredAt >= filters.From.Value.Date);
        }

        if (filters.To.HasValue)
        {
            query = query.Where(log => log.OccurredAt < filters.To.Value.Date.AddDays(1));
        }

        if (!string.IsNullOrWhiteSpace(filters.UserName))
        {
            var userName = filters.UserName.Trim();
            query = query.Where(log => log.UserName != null && EF.Functions.ILike(log.UserName, $"%{userName}%"));
        }

        if (!string.IsNullOrWhiteSpace(filters.Action) &&
            Enum.TryParse<AuditAction>(filters.Action, out var action))
        {
            query = query.Where(log => log.Action == action);
        }

        if (!string.IsNullOrWhiteSpace(filters.EntityName))
        {
            query = query.Where(log => log.EntityName == filters.EntityName);
        }

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            var search = filters.Search.Trim();
            query = query.Where(log =>
                (log.Details != null && EF.Functions.ILike(log.Details, $"%{search}%")) ||
                (log.CorrelationId != null && EF.Functions.ILike(log.CorrelationId, $"%{search}%")) ||
                (log.EntityId != null && EF.Functions.ILike(log.EntityId, $"%{search}%")) ||
                (log.IpAddress != null && EF.Functions.ILike(log.IpAddress, $"%{search}%")));
        }

        return query;
    }

    private static string BuildCsv(IReadOnlyCollection<AuditLogItemViewModel> logs)
    {
        var builder = new StringBuilder();
        builder.AppendLine("DataHoraUtc;Acao;Entidade;IdEntidade;Usuario;IP;CorrelationId;Detalhes");

        foreach (var log in logs)
        {
            builder
                .Append(EscapeCsv(log.OccurredAt.ToString("O"))).Append(';')
                .Append(EscapeCsv(log.Action)).Append(';')
                .Append(EscapeCsv(log.EntityName)).Append(';')
                .Append(EscapeCsv(log.EntityId)).Append(';')
                .Append(EscapeCsv(log.UserName)).Append(';')
                .Append(EscapeCsv(log.IpAddress)).Append(';')
                .Append(EscapeCsv(log.CorrelationId)).Append(';')
                .Append(EscapeCsv(log.Details))
                .AppendLine();
        }

        return builder.ToString();
    }

    private static string EscapeCsv(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var sanitized = value.ReplaceLineEndings(" ");

        if (sanitized.StartsWith('=') ||
            sanitized.StartsWith('+') ||
            sanitized.StartsWith('-') ||
            sanitized.StartsWith('@'))
        {
            sanitized = $"'{sanitized}";
        }

        return $"\"{sanitized.Replace("\"", "\"\"")}\"";
    }

    private static string DescribeFilters(AuditFilterViewModel filters)
    {
        var parts = new[]
        {
            filters.From.HasValue ? $"From={filters.From:yyyy-MM-dd}" : null,
            filters.To.HasValue ? $"To={filters.To:yyyy-MM-dd}" : null,
            !string.IsNullOrWhiteSpace(filters.UserName) ? $"UserName={filters.UserName}" : null,
            !string.IsNullOrWhiteSpace(filters.Action) ? $"Action={filters.Action}" : null,
            !string.IsNullOrWhiteSpace(filters.EntityName) ? $"EntityName={filters.EntityName}" : null,
            !string.IsNullOrWhiteSpace(filters.Search) ? "Search=***" : null
        }.Where(part => part is not null);

        return string.Join("; ", parts);
    }
}
