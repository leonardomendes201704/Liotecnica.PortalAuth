namespace Liotecnica.PortalAuth.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public string? CreatedBy { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public string? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }

    public void MarkAsCreated(string? userName, DateTime? date = null)
    {
        CreatedBy = userName;
        CreatedAt = date ?? DateTime.UtcNow;
    }

    public void MarkAsUpdated(string? userName, DateTime? date = null)
    {
        UpdatedBy = userName;
        UpdatedAt = date ?? DateTime.UtcNow;
    }

    public void MarkAsDeleted(string? userName, DateTime? date = null)
    {
        IsDeleted = true;
        MarkAsUpdated(userName, date);
    }
}
