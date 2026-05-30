namespace Liotecnica.PortalAuth.Web.Models;

public sealed class OperationalStatusViewModel
{
    public string OverallStatus { get; init; } = "Unknown";
    public DateTime CheckedAt { get; init; } = DateTime.UtcNow;
    public TimeSpan TotalDuration { get; init; }
    public IReadOnlyCollection<OperationalComponentViewModel> Components { get; init; } = [];
    public IReadOnlyCollection<OperationalStatusHistoryItemViewModel> History { get; init; } = [];
    public IReadOnlyCollection<OperationalAlertViewModel> Alerts { get; init; } = [];
}

public sealed record OperationalComponentViewModel(
    string Name,
    string Status,
    string Description,
    string Duration,
    IReadOnlyCollection<string> Tags);

public sealed record OperationalStatusHistoryItemViewModel(
    DateTime CheckedAt,
    string Status,
    long TotalDurationMilliseconds,
    string ComponentsSummary);

public sealed record OperationalAlertViewModel(
    string Severity,
    string Title,
    string Description);
