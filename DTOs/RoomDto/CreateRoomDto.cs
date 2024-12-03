namespace SmartHomeServer.DTOs.RoomDto;

public sealed record CreateRoomDto(
    string RoomName,
    string RoomDescription,
    Guid AppUserId);
