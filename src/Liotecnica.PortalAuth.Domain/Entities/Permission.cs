using Liotecnica.PortalAuth.Domain.Common;

namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class Permission : BaseEntity
{
    private Permission()
    {
        Code = string.Empty;
        Description = string.Empty;
        Module = string.Empty;
    }

    public Permission(string code, string description, string module, bool isCritical)
    {
        Code = code;
        Description = description;
        Module = module;
        IsCritical = isCritical;
        IsActive = true;
    }

    public string Code { get; private set; }
    public string Description { get; private set; }
    public string Module { get; private set; }
    public bool IsCritical { get; private set; }
    public bool IsActive { get; private set; }

    public void Update(string code, string description, string module, bool isCritical, bool isActive, string? updatedBy)
    {
        Code = code;
        Description = description;
        Module = module;
        IsCritical = isCritical;
        IsActive = isActive;
        MarkAsUpdated(updatedBy);
    }
}
