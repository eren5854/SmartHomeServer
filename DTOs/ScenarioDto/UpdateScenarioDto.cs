using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.ScenarioDto;

public sealed record UpdateScenarioDto(
    Guid Id,
    Guid AppUserId,
    string ScenarioName,
    string? ScenarioDescription,
    Guid? TriggerSensorId,
    TriggerTypeEnum TriggerType,
    decimal? TriggerValue,
    DateTime? TriggerTime,
    Guid ActionSensorId,
    ActionTypeEnum ActionType,
    double ActionValue);