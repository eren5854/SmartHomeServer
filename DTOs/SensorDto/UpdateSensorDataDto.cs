namespace SmartHomeServer.DTOs.SensorDto;

public sealed record UpdateSensorDataDto(
    string SerialNo,
    double Data1,
    double? Data2,
    double? Data3,
    double? Data4,
    string? Data5,
    string? Data6);
