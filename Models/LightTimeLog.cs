using SmartHomeServer.Entities;

namespace SmartHomeServer.Models;

public sealed class LightTimeLog : Entity
{
    public Guid SensorId { get; set; }
    public Sensor Sensor { get; set; } = default!;

    public DateTime Date { get; set; }
    public DateTime RelayOnDuration { get; set; }
    
    
}
