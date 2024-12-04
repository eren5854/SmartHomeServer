using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.TriggerDto;

public sealed record CreateTriggerDto(
    Guid ScenarioId,
    Guid SensorId,
    TriggerTypeEnum TriggerType,
    DateTime TriggerTime);
