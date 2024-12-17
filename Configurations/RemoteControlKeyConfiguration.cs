using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class RemoteControlKeyConfiguration : IEntityTypeConfiguration<RemoteControlKey>
{
    public void Configure(EntityTypeBuilder<RemoteControlKey> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
