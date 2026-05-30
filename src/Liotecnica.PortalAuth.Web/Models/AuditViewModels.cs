namespace Liotecnica.PortalAuth.Web.Models;

public sealed class AuditIndexViewModel
{
    public IReadOnlyCollection<AuditLogItemViewModel> Logs { get; init; } = [];
    public AuditFilterViewModel Filters { get; init; } = new();
    public IReadOnlyCollection<string> AvailableActions { get; init; } = [];
    public IReadOnlyCollection<string> AvailableEntities { get; init; } = [];
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 25;
    public int TotalItems { get; init; }
    public int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
    public AuditRetentionViewModel Retention { get; init; } = new();
}

public sealed class AuditFilterViewModel
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? UserName { get; set; }
    public string? Action { get; set; }
    public string? EntityName { get; set; }
    public string? Search { get; set; }
}

public sealed class AuditRetentionViewModel
{
    public int RetentionDays { get; set; } = 180;
    public DateTime CutoffDate => DateTime.UtcNow.AddDays(-RetentionDays);
}

public sealed record AuditLogItemViewModel(
    DateTime OccurredAt,
    string Action,
    string EntityName,
    string? EntityId,
    string? UserName,
    string? IpAddress,
    string? CorrelationId,
    string? Details);
