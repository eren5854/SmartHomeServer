using SmartHomeServer.Enums;

namespace SmartHomeServer.DTOs.SensorDto;

public sealed record GetAllSensorDto(
    Guid Id,
    string SensorName,
    string SecretKey,
    string SerialNo,
    string Status,
    SensorTypeEnum SensorType,
    double? Data1,
    double? Data2,
    double? Data3,
    double? Data4,
    string? Data5,
    string? Data6);
