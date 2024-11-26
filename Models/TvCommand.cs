using SmartHomeServer.Entities;

namespace SmartHomeServer.Models;

public sealed class TvCommand : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SerialNo { get; set; } = string.Empty;
    public string? Status { get; set; }

    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public bool? OnOff { get; set; }
    public bool? NextChannel { get; set; }
    public bool? PrevChannel { get; set; }
    public bool? VolumeUp { get; set; }
    public bool? VolumeDown { get; set; }
    public bool? ChannelMenu { get; set; }
    public bool? Source { get; set; }
}
