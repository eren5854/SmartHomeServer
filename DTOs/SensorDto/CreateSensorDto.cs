using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.SensorDto;

public sealed record CreateSensorDto(
    string SensorName,
    string? Description,
    //string SerialNo,
    SensorTypeEnum SensorType,
    Guid? AppUserId);
