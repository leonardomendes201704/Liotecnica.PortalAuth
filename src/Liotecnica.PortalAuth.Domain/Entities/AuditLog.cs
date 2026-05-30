using Liotecnica.PortalAuth.Domain.Enums;

namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class AuditLog
{
    private AuditLog()
    {
    }

    public AuditLog(
        AuditAction action,
        string entityName,
        string? entityId,
        string? userId,
        string? userName,
        string? ipAddress,
        string? correlationId,
        string? details)
    {
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        UserId = userId;
        UserName = userName;
        IpAddress = ipAddress;
        CorrelationId = correlationId;
        Details = details;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; private set; } = DateTime.UtcNow;
    public AuditAction Action { get; private set; }
    public string EntityName { get; private set; } = string.Empty;
    public string? EntityId { get; private set; }
    public string? UserId { get; private set; }
    public string? UserName { get; private set; }
    public string? IpAddress { get; private set; }
    public string? CorrelationId { get; private set; }
    public string? Details { get; private set; }
}
