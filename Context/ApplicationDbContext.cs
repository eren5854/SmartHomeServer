﻿using ED.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Models;

namespace SmartHomeServer.Context;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<TvCommand> TvCommands { get; set; }

    public DbSet<Scenario> Scenarios { get; set; }
    public DbSet<Trigger> Triggers { get; set; }
    public DbSet<Models.Action> Actions { get; set; }

    public DbSet<LightTimeLog> LightTimeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Sensor>()
            .HasOne(sensor => sensor.Room)
            .WithMany(room => room.Sensors)
            .HasForeignKey(sensor => sensor.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Scenario>()
            .HasOne(p => p.Trigger)
            .WithOne()
            .HasForeignKey<Scenario>(p => p.TriggerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Trigger>()
            .HasOne(p => p.Action)
            .WithOne()
            .HasForeignKey<Trigger>(p => p.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.Entity<Trigger>()
        //    .HasOne(trigger => trigger.Scenario)
        //    .WithMany(scenario => scenario.Triggers)
        //    .HasForeignKey(trigger => trigger.ScenarioId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.Entity<Models.Action>()
        //    .HasOne(action => action.Scenario)
        //    .WithMany(scenario => scenario.Actions)
        //    .HasForeignKey(action => action.ScenarioId)
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
