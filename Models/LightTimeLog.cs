using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class LightTimeLog : Entity
{
    //public object? SensorInfo => new
    //{
    //    SensorId = SensorId,
    //    SensorName = Sensor!.SensorName,
    //};

    [JsonIgnore]
    public Guid SensorId { get; set; }
    [JsonIgnore]
    public Sensor Sensor { get; set; } = default!;

    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }

    public double? TimeCount { get; set; }
    public int? OpenCount { get; set; }    
    
}
