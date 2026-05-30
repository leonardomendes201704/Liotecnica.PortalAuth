using Microsoft.AspNetCore.Identity;

namespace Liotecnica.PortalAuth.Infrastructure.Identity;

public sealed class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }

    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public void Deactivate(string? updatedBy)
    {
        IsActive = false;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate(string? updatedBy)
    {
        IsActive = true;
        IsDeleted = false;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted(string? updatedBy)
    {
        IsActive = false;
        IsDeleted = true;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}
