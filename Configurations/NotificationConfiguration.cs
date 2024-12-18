using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(p => p.NotificationName)
            .HasColumnType("varchar(300)")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.NotificationMessage)
            .HasColumnType("varchar(1500)")
            .HasMaxLength(1500)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
