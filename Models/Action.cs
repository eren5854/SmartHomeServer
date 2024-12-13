using SmartHomeServer.Entities;
using SmartHomeServer.Enums;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Action : Entity
{
    //public Guid ScenarioId { get; set; } = default!;
    //public Scenario Scenario { get; set; } = default!;

    public object? SensorInfo => new
    {
        SensorId = SensorId,
        SensorName = Sensor?.SensorName,
        SerialNo = Sensor?.SerialNo,
        Data1 = Sensor?.Data1
    };

    [JsonIgnore]
    public Guid SensorId { get; set; } = default!;
    [JsonIgnore]
    public Sensor Sensor { get; set; } = default!;

    public ActionTypeEnum ActionType { get; set; }

    public double Value { get; set; } = default!;
}
