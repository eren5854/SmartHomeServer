using SmartHomeServer.DTOs.RemoteControlKeyDto;

namespace SmartHomeServer.DTOs.RemoteControlDto;

public sealed record CreateRemoteControlDto(
    string Name,
    string Description,
    //List<CreateRemoteControlKeyDto> CreateRemoteControlKeys,
    Guid AppUserId);
