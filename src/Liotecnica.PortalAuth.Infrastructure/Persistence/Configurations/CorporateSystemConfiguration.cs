using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class CorporateSystemConfiguration : IEntityTypeConfiguration<CorporateSystem>
{
    public void Configure(EntityTypeBuilder<CorporateSystem> builder)
    {
        builder.ToTable("corporate_systems");

        builder.HasKey(system => system.Id);

        builder.Property(system => system.Id)
            .HasColumnName("id");

        builder.Property(system => system.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(system => system.Code)
            .HasColumnName("code")
            .HasMaxLength(80)
            .IsRequired();

        builder.HasIndex(system => system.Code)
            .IsUnique();

        builder.Property(system => system.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(system => system.BaseUrl)
            .HasColumnName("base_url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(system => system.Icon)
            .HasColumnName("icon")
            .HasMaxLength(120);

        builder.Property(system => system.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(system => system.RequiresMfa)
            .HasColumnName("requires_mfa")
            .IsRequired();

        builder.Property(system => system.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(system => system.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(120);

        builder.Property(system => system.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(system => system.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(120);

        builder.Property(system => system.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}
