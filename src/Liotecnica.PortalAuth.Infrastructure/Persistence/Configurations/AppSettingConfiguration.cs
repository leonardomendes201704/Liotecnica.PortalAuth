using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
{
    public void Configure(EntityTypeBuilder<AppSetting> builder)
    {
        builder.ToTable("app_settings");

        builder.HasKey(setting => setting.Id);

        builder.Property(setting => setting.Id)
            .HasColumnName("id");

        builder.Property(setting => setting.Key)
            .HasColumnName("key")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(setting => setting.Value)
            .HasColumnName("value")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(setting => setting.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(setting => setting.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(setting => setting.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(256);

        builder.HasIndex(setting => setting.Key)
            .IsUnique();
    }
}
