using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class RemoteControl : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string SerialNo { get; set; } = string.Empty;
    public string? SecretKey { get; set; }

    public string? Status { get; set; }

    [JsonIgnore]
    public Guid? AppUserId { get; set; }
    [JsonIgnore]
    public AppUser? AppUser { get; set; }

    public List<RemoteControlKey>? RemoteControlKeys { get; set; }

    //public bool? OnOff { get; set; }
    //public bool? NextChannel { get; set; }
    //public bool? PrevChannel { get; set; }
    //public bool? VolumeUp { get; set; }
    //public bool? VolumeDown { get; set; }
    //public bool? ChannelMenu { get; set; }
    //public bool? Source { get; set; }
}
