using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs");

        builder.HasKey(log => log.Id);

        builder.Property(log => log.Id)
            .HasColumnName("id");

        builder.Property(log => log.OccurredAt)
            .HasColumnName("occurred_at")
            .IsRequired();

        builder.Property(log => log.Action)
            .HasColumnName("action")
            .HasConversion<string>()
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(log => log.EntityName)
            .HasColumnName("entity_name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(log => log.EntityId)
            .HasColumnName("entity_id")
            .HasMaxLength(120);

        builder.Property(log => log.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(120);

        builder.Property(log => log.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(256);

        builder.Property(log => log.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(80);

        builder.Property(log => log.CorrelationId)
            .HasColumnName("correlation_id")
            .HasMaxLength(120);

        builder.Property(log => log.Details)
            .HasColumnName("details")
            .HasMaxLength(1000);

        builder.HasIndex(log => log.OccurredAt);
        builder.HasIndex(log => log.Action);
        builder.HasIndex(log => log.EntityName);
        builder.HasIndex(log => log.UserName);
    }
}
