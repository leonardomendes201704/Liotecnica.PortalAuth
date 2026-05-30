using Liotecnica.PortalAuth.Domain.Common;

namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class CorporateSystem : BaseEntity
{
    private CorporateSystem()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    public CorporateSystem(
        string name,
        string code,
        string? description,
        string baseUrl,
        string? icon,
        bool requiresMfa)
    {
        Name = name;
        Code = code;
        Description = description;
        BaseUrl = baseUrl;
        Icon = icon;
        RequiresMfa = requiresMfa;
        IsActive = true;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public string BaseUrl { get; private set; } = string.Empty;
    public string? Icon { get; private set; }
    public bool IsActive { get; private set; }
    public bool RequiresMfa { get; private set; }

    public void Update(
        string name,
        string code,
        string? description,
        string baseUrl,
        string? icon,
        bool requiresMfa,
        bool isActive,
        string? updatedBy)
    {
        Name = name;
        Code = code;
        Description = description;
        BaseUrl = baseUrl;
        Icon = icon;
        RequiresMfa = requiresMfa;
        IsActive = isActive;
        MarkAsUpdated(updatedBy);
    }
}
