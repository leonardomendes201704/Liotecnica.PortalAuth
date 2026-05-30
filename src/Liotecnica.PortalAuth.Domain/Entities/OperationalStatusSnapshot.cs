namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class OperationalStatusSnapshot
{
    private OperationalStatusSnapshot()
    {
    }

    public OperationalStatusSnapshot(
        string status,
        DateTime checkedAt,
        long totalDurationMilliseconds,
        string componentsSummary)
    {
        Id = Guid.NewGuid();
        Status = status;
        CheckedAt = checkedAt;
        TotalDurationMilliseconds = totalDurationMilliseconds;
        ComponentsSummary = componentsSummary;
    }

    public Guid Id { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTime CheckedAt { get; private set; }
    public long TotalDurationMilliseconds { get; private set; }
    public string ComponentsSummary { get; private set; } = string.Empty;
}
