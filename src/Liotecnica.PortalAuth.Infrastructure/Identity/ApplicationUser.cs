using Microsoft.AspNetCore.Identity;

namespace Liotecnica.PortalAuth.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = string.Empty;
    public string? Department { get; set; }
}
