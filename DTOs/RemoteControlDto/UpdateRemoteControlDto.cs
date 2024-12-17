using SmartHomeServer.DTOs.RemoteControlKeyDto;

namespace SmartHomeServer.DTOs.RemoteControlDto;

public sealed record UpdateRemoteControlDto(
    Guid Id,
    string Name,
    string Description
    //List<UpdateRemoteControlKeyDto> UpdateRemoteControlKeys,
    //Guid AppUserId
    );
