using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Web.Services;

public sealed class AuditRetentionService : IAuditRetentionService
{
    public const string RetentionDaysKey = "Audit.RetentionDays";
    public const int DefaultRetentionDays = 180;
    public const int MinRetentionDays = 30;
    public const int MaxRetentionDays = 3650;

    private const string RetentionDescription = "Quantidade de dias para manter logs de auditoria.";

    private readonly PortalAuthDbContext _dbContext;

    public AuditRetentionService(PortalAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetRetentionDaysAsync(CancellationToken cancellationToken = default)
    {
        var setting = await GetOrCreateRetentionSettingAsync(cancellationToken);

        return ParseRetentionDays(setting.Value);
    }

    public async Task SetRetentionDaysAsync(
        int retentionDays,
        string? updatedBy,
        CancellationToken cancellationToken = default)
    {
        retentionDays = ClampRetentionDays(retentionDays);

        var setting = await GetOrCreateRetentionSettingAsync(cancellationToken);
        setting.Update(retentionDays.ToString(), RetentionDescription, updatedBy);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuditRetentionResult> PurgeExpiredAsync(CancellationToken cancellationToken = default)
    {
        var retentionDays = await GetRetentionDaysAsync(cancellationToken);
        var cutoffDate = DateTime.UtcNow.AddDays(-retentionDays);

        var deletedCount = await _dbContext.AuditLogs
            .Where(log => log.OccurredAt < cutoffDate)
            .ExecuteDeleteAsync(cancellationToken);

        return new AuditRetentionResult(retentionDays, cutoffDate, deletedCount);
    }

    private async Task<AppSetting> GetOrCreateRetentionSettingAsync(CancellationToken cancellationToken)
    {
        var setting = await _dbContext.AppSettings
            .SingleOrDefaultAsync(existing => existing.Key == RetentionDaysKey, cancellationToken);

        if (setting is not null)
        {
            return setting;
        }

        setting = new AppSetting(
            RetentionDaysKey,
            DefaultRetentionDays.ToString(),
            RetentionDescription,
            "system");

        _dbContext.AppSettings.Add(setting);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return setting;
    }

    private static int ParseRetentionDays(string value)
    {
        return int.TryParse(value, out var parsed)
            ? ClampRetentionDays(parsed)
            : DefaultRetentionDays;
    }

    private static int ClampRetentionDays(int retentionDays)
    {
        return Math.Clamp(retentionDays, MinRetentionDays, MaxRetentionDays);
    }
}
