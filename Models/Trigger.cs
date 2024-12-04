using SmartHomeServer.Entities;
using SmartHomeServer.Enums;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Trigger : Entity
{
    //public Guid ScenarioId { get; set; }
    //public Scenario Scenario { get; set; } = default!;

    public object ActionInfo => new
    {
        ActionId = ActionId,
        ActionSensorId = Action!.SensorId,
        Actiontype = Action.ActionType,
        ActionValue = Action.Value,
    };

    [JsonIgnore]
    public Guid ActionId { get; set; }
    [JsonIgnore]
    public Action? Action { get; set; }

    public Guid? SensorId { get; set; }
    public Sensor? Sensor { get; set; } = default!;

    public TriggerTypeEnum TriggerType { get; set; }

    public double? TriggerValue { get; set; }

    public DateTime? TriggerTime { get; set; }
}
