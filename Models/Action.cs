using SmartHomeServer.Entities;
using SmartHomeServer.Enums;

namespace SmartHomeServer.Models;

public sealed class Action : Entity
{
    public Guid ScenarioId { get; set; } = default!;
    public Scenario Scenario { get; set; } = default!;

    public Guid SensorId { get; set; } = default!;
    public Sensor Sensor { get; set; } = default!;

    public ActionTypeEnum ActionType { get; set; }

    public double Value { get; set; } = default!;
}
