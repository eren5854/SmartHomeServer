using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.SensorDto;

public sealed record UpdateSensorDto(
    Guid Id,
    string SensorName,
    string? Description,
    string SerialNo,
    SensorTypeEnum SensorType
    //Guid AppUserId
    );
