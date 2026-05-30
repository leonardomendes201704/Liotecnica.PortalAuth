namespace Liotecnica.PortalAuth.Web.Services;

public sealed record AuditRetentionResult(
    int RetentionDays,
    DateTime CutoffDate,
    int DeletedCount);
