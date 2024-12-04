namespace SmartHomeServer.DTOs.ScenarioDto;

public sealed record CreateScenarioDto(
    string ScenarioName,
    string ScenarioDescription,
    Guid AppUserId);
