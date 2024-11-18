using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder
            .Property(p => p.SensorName)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(300)")
            .HasMaxLength(100);

        builder
            .Property(p => p.SerialNo)
            .IsRequired()
            .HasColumnType("varchar(16)")
            .HasMaxLength(16);

        builder
            .Property(p => p.CreatedBy)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder
           .Property(p => p.UpdatedBy)
           .HasColumnType("varchar(50)")
           .HasMaxLength(50);

        builder
           .Property(p => p.Data5)
           .HasColumnType("varchar(100)")
           .HasMaxLength(100);

        builder
          .Property(p => p.Data6)
          .HasColumnType("varchar(100)")
          .HasMaxLength(100);

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
