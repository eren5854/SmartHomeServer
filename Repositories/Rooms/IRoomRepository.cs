using ED.Result;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Rooms;

public interface IRoomRepository
{
    public Task<Result<string>> Create(Room room, CancellationToken cancellationToken);
    public Task<Result<List<GetRoomDto>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<GetRoomDto>> GetById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
    public Room? GetById(Guid Id);
    public Task<Result<string>> Update(Room room, CancellationToken cancellationToken);
    public Task<Result<List<Room>>> GetAll(CancellationToken cancellationToken);
}
