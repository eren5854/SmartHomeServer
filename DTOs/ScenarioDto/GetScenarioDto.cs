using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.ScenarioDto;

public sealed record GetScenarioDto(
    Guid Id,
    Guid? AppUserId,
    string ScenarioName,
    string ScenarioDescription,
    Guid TriggerSensorId,
    TriggerTypeEnum TriggerType,
    double? TriggerValue,
    DateTime? TriggerTime,
    Guid ActionSensorId,
    ActionTypeEnum ActionType,
    double ActionValue);
