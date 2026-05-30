using Liotecnica.PortalAuth.Domain.Entities;
using Liotecnica.PortalAuth.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class RoleSystemAccessConfiguration : IEntityTypeConfiguration<RoleSystemAccess>
{
    public void Configure(EntityTypeBuilder<RoleSystemAccess> builder)
    {
        builder.ToTable("role_system_access");

        builder.HasKey(access => new { access.RoleId, access.SystemId });

        builder.Property(access => access.RoleId)
            .HasColumnName("role_id");

        builder.Property(access => access.SystemId)
            .HasColumnName("system_id");

        builder.HasOne(access => access.System)
            .WithMany()
            .HasForeignKey(access => access.SystemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ApplicationRole>()
            .WithMany()
            .HasForeignKey(access => access.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
