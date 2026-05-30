namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class AppSetting
{
    private AppSetting()
    {
    }

    public AppSetting(string key, string value, string? description, string? updatedBy)
    {
        Key = key;
        Value = value;
        Description = description;
        UpdatedBy = updatedBy;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Key { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; private set; }

    public void Update(string value, string? description, string? updatedBy)
    {
        Value = value;
        Description = description;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}
