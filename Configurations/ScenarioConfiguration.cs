﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHomeServer.Models;

namespace SmartHomeServer.Configurations;

public sealed class ScenarioConfiguration : IEntityTypeConfiguration<Scenario>
{
    public void Configure(EntityTypeBuilder<Scenario> builder)
    {
        builder.Property(p => p.ScenarioName)
            .HasColumnType("varchar(300)")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.ScenarioDescription)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
