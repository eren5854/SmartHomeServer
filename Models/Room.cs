using SmartHomeServer.Entities;

namespace SmartHomeServer.Models;

public sealed class Room : Entity
{
    public string RoomName { get; set; } = default!;
    public string? RoomDescription { get; set; }
    public List<Sensor>? Sensors { get; set; }

    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
