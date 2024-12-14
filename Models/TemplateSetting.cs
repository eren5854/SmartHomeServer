using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class TemplateSetting : Entity
{
    public string? ContainerLayout { get; set; }
    public string? HeaderBg { get; set; }
    public string? HeaderPosition { get; set; }
    public string? Layout { get; set; }
    public string? NavHeaderBg { get; set; }
    public string? Primary { get; set; }
    public string? SidebarBg { get; set; }
    public string? SidebarPosition { get; set; }
    public string? SidebarStyle { get; set; }
    public string? Typography { get; set; }
    public string? Version { get; set; }

    [JsonIgnore]
    public Guid? AppUserId { get; set; }
    [JsonIgnore]
    public AppUser? AppUser { get; set; }
}
