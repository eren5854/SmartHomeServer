using SmartHomeServer.Entities;
using SmartHomeServer.Enums;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Notification : Entity
{
    public string NotificationName { get; set; } = default!;
    public string NotificationMessage { get; set; } = default!;
    public NotificationTypeEnum NotificationType { get; set; }

    [JsonIgnore]
    public Guid? AppUserId { get; set; }
    [JsonIgnore]
    public AppUser? AppUser { get; set; }
}
