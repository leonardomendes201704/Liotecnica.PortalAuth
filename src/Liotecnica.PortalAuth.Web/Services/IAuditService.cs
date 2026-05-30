using Liotecnica.PortalAuth.Domain.Enums;

namespace Liotecnica.PortalAuth.Web.Services;

public interface IAuditService
{
    Task RecordAsync(
        AuditAction action,
        string entityName,
        string? entityId = null,
        string? details = null,
        string? userId = null,
        string? userName = null,
        CancellationToken cancellationToken = default);
}
