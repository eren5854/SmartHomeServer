using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.TriggerDto;

public sealed record CreateTriggerDto(
    Guid ScenarioId,
    Guid SensorId,
    TriggerTypeEnum TriggerType,
    decimal? TriggerValue,
    DateTime? TriggerTime);
