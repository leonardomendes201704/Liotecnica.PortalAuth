namespace Liotecnica.PortalAuth.Web.Services;

public interface IAuditRetentionService
{
    Task<int> GetRetentionDaysAsync(CancellationToken cancellationToken = default);
    Task SetRetentionDaysAsync(int retentionDays, string? updatedBy, CancellationToken cancellationToken = default);
    Task<AuditRetentionResult> PurgeExpiredAsync(CancellationToken cancellationToken = default);
}
