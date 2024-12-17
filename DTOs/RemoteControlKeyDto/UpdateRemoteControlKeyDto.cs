namespace SmartHomeServer.DTOs.RemoteControlKeyDto;

public sealed record UpdateRemoteControlKeyDto(
    Guid Id,
    string KeyName,
    string KeyCode,
    bool KeyValue);
