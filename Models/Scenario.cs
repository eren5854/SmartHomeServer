﻿using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class Scenario : Entity
{
    [JsonIgnore]
    public Guid AppUserId { get; set; } = default!;
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public string ScenarioName { get; set; } = default!;
    public string? ScenarioDescription { get; set; }

    public object TriggerInfo => new
    {
        TriggerId = TriggerId,
        TriggerSensorInfo = Trigger?.SensorInfo,
        TriggerType = Trigger!.TriggerType,
        TriggerValue = Trigger.TriggerValue,
        TriggerTime = Trigger.TriggerTime,
        ActionInfo = Trigger.ActionInfo
    };

    [JsonIgnore]
    public Guid TriggerId { get; set; }
    [JsonIgnore]
    public Trigger? Trigger { get; set; }

    //public List<Trigger>? Triggers { get; set; }
    //public List<Action>? Actions { get; set; }
}
