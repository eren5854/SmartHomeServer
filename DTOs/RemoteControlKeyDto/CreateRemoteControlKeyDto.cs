namespace SmartHomeServer.DTOs.RemoteControlKeyDto;

public sealed record CreateRemoteControlKeyDto(
    string KeyName,
    string KeyCode
   );
