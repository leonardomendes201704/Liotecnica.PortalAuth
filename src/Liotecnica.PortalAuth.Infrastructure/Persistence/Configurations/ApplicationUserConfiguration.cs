using Liotecnica.PortalAuth.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.MustChangePassword)
            .HasColumnName("must_change_password")
            .HasDefaultValue(false)
            .IsRequired();
    }
}
