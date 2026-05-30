using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence;

public sealed class PortalAuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public PortalAuthDbContext(DbContextOptions<PortalAuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<CorporateSystem> CorporateSystems => Set<CorporateSystem>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<RoleSystemAccess> RoleSystemAccesses => Set<RoleSystemAccess>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("portal_auth");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalAuthDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
