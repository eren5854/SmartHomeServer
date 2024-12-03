namespace SmartHomeServer.DTOs.RoomDto;

public sealed record UpdateRoomDto(
    Guid Id,
    string RoomName,
    string RoomDescription,
    Guid AppUserId);
