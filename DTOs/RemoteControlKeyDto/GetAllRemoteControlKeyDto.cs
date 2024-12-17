namespace SmartHomeServer.DTOs.RemoteControlKeyDto;

public sealed record GetAllRemoteControlKeyDto(
    Guid Id,
    string KeyName,
    string KeyCode,
    bool KeyValue,
    int KeyQueue);
