using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.SensorDto;

public sealed record CreateSensorDto(
    string SensorName,
    string? Description,
    SensorTypeEnum SensorType,
    Guid? AppUserId,
    Guid? RoomId);
