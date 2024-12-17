using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class RemoteControlConfiguration : IEntityTypeConfiguration<RemoteControl>
{
    public void Configure(EntityTypeBuilder<RemoteControl> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
