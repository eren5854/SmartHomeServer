using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class RemoteControlKey : Entity
{
    public string KeyName { get; set; } = default!;
    public string KeyCode { get; set; } = default!;
    public bool KeyValue { get; set; }
    public int KeyQueue { get; set; } = default!;

    //public object RemoteControlInfo => new
    //{
    //    Id = RemoteControlId,
    //    Name = RemoteControl.Name,
    //    SerialNo = RemoteControl.SerialNo,
    //};

    [JsonIgnore]
    public Guid RemoteControlId { get; set; } = default!;
    [JsonIgnore]
    public RemoteControl RemoteControl { get; set; } = default!;
}
