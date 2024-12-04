using SmartHomeServer.Entities;
using SmartHomeServer.Enums;

namespace SmartHomeServer.Models;

public sealed class Trigger : Entity
{
    public Guid ScenarioId { get; set; }
    public Scenario Scenario { get; set; } = default!;

    public Guid? SensorId { get; set; }
    public Sensor? Sensor { get; set; } = default!;

    public TriggerTypeEnum TriggerType { get; set; }

    public DateTime TriggerTime { get; set; }
}
