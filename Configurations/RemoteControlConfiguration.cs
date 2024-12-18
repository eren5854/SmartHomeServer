using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class RemoteControlConfiguration : IEntityTypeConfiguration<RemoteControl>
{
    public void Configure(EntityTypeBuilder<RemoteControl> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
