using Liotecnica.PortalAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Configurations;

public sealed class OperationalStatusSnapshotConfiguration : IEntityTypeConfiguration<OperationalStatusSnapshot>
{
    public void Configure(EntityTypeBuilder<OperationalStatusSnapshot> builder)
    {
        builder.ToTable("operational_status_snapshots");

        builder.HasKey(snapshot => snapshot.Id);

        builder.Property(snapshot => snapshot.Id)
            .HasColumnName("id");

        builder.Property(snapshot => snapshot.Status)
            .HasColumnName("status")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(snapshot => snapshot.CheckedAt)
            .HasColumnName("checked_at")
            .IsRequired();

        builder.Property(snapshot => snapshot.TotalDurationMilliseconds)
            .HasColumnName("total_duration_ms")
            .IsRequired();

        builder.Property(snapshot => snapshot.ComponentsSummary)
            .HasColumnName("components_summary")
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasIndex(snapshot => snapshot.CheckedAt);
    }
}
