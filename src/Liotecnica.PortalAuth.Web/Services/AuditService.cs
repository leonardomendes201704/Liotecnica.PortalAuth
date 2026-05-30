using System.Security.Claims;
using Liotecnica.BuildingBlocks.Logging.Correlation;
using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Domain.Enums;
using Liotecnica.PortalAuth.Infrastructure.Persistence;

namespace Liotecnica.PortalAuth.Web.Services;

public sealed class AuditService : IAuditService
{
    private readonly PortalAuthDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICorrelationIdAccessor _correlationIdAccessor;

    public AuditService(
        PortalAuthDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        ICorrelationIdAccessor correlationIdAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _correlationIdAccessor = correlationIdAccessor;
    }

    public async Task RecordAsync(
        AuditAction action,
        string entityName,
        string? entityId = null,
        string? details = null,
        string? userId = null,
        string? userName = null,
        CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        var log = new AuditLog(
            action,
            TruncateRequired(entityName, 120),
            Truncate(entityId, 120),
            Truncate(userId ?? user?.FindFirstValue(ClaimTypes.NameIdentifier), 120),
            Truncate(userName ?? user?.Identity?.Name, 256),
            Truncate(httpContext?.Connection.RemoteIpAddress?.ToString(), 80),
            Truncate(_correlationIdAccessor.CorrelationId, 120),
            Truncate(details, 1000));

        _dbContext.AuditLogs.Add(log);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return value.Length <= maxLength ? value : value[..maxLength];
    }

    private static string TruncateRequired(string value, int maxLength)
    {
        return value.Length <= maxLength ? value : value[..maxLength];
    }
}
