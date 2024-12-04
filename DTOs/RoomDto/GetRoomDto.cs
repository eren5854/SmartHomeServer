using SmartHomeServer.DTOs.SensorDto;

namespace SmartHomeServer.DTOs.RoomDto;

public sealed record GetRoomDto(
    Guid Id,
    string RoomName,
    string RoomDescription,
    List<GetAllSensorDto> GetAllSensor);
