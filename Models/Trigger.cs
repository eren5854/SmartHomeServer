using SmartHomeServer.Entities;
using SmartHomeServer.Enums;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Trigger : Entity
{
    //public Guid ScenarioId { get; set; }
    //public Scenario Scenario { get; set; } = default!;

    public object? ActionInfo => new
    {
        ActionId = ActionId,
        ActionSensorInfo = Action!.SensorInfo,
        Actiontype = Action.ActionType,
        ActionValue = Action.Value,
    };

    [JsonIgnore]
    public Guid ActionId { get; set; }
    [JsonIgnore]
    public Action? Action { get; set; }


    public object? SensorInfo => new
    {
        SensorId = SensorId,
        SensorName = Sensor?.SensorName,
        SerialNo = Sensor?.SerialNo,
        Data1 = Sensor?.Data1
    };

    public Guid? SensorId { get; set; }
    [JsonIgnore]
    public Sensor? Sensor { get; set; } = default!;

    public TriggerTypeEnum TriggerType { get; set; }

    public double? TriggerValue { get; set; }

    public DateTime? TriggerTime { get; set; }
}
