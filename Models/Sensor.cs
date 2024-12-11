using SmartHomeServer.Entities;
using SmartHomeServer.Enums;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Sensor : Entity
{
    public string SensorName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SerialNo { get; set; } = string.Empty;
    public string? Status { get; set; }

    public string? SecretKey { get; set; }

    public SensorTypeEnum SensorType { get; set; } = SensorTypeEnum.Other;

    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public object? RoomInfo => new
    {
        RoomId = RoomId,
        RoomName = Room!.RoomName,
        RoomDescription = Room.RoomDescription,
    };

    [JsonIgnore]
    public Guid? RoomId { get; set; }
    [JsonIgnore]
    public Room? Room { get; set; }

    public List<LightTimeLog>? LightTimeLogs { get; set; }

    public double? Data1 { get; set; }
    public double? Data2 { get; set; }
    public double? Data3 { get; set; }
    public double? Data4 { get; set; }
    public string? Data5 { get; set; }
    public string? Data6 { get; set; }

}
