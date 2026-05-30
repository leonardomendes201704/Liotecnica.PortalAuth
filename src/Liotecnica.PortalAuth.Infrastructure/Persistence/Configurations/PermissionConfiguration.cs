using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(permission => permission.Id);

        builder.Property(permission => permission.Id)
            .HasColumnName("id");

        builder.Property(permission => permission.Code)
            .HasColumnName("code")
            .HasMaxLength(120)
            .IsRequired();

        builder.HasIndex(permission => permission.Code)
            .IsUnique();

        builder.Property(permission => permission.Description)
            .HasColumnName("description")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(permission => permission.Module)
            .HasColumnName("module")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(permission => permission.IsCritical)
            .HasColumnName("is_critical")
            .IsRequired();

        builder.Property(permission => permission.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(permission => permission.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(permission => permission.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(120);

        builder.Property(permission => permission.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(permission => permission.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(120);

        builder.Property(permission => permission.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();
    }
}
