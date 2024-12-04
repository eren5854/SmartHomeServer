using SmartHomeServer.Entities;

namespace SmartHomeServer.Models;

public sealed class Scenario : Entity
{
    public Guid AppUserId { get; set; } = default!;
    public AppUser AppUser { get; set; } = default!;

    public string ScenarioName { get; set; } = default!;
    public string? ScenarioDescription { get; set; }

    public List<Trigger>? Triggers { get; set; }
    public List<Action>? Actions { get; set; }
}
