using ED.Result;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.Rooms;

public interface IRoomService
{
    public Task<Result<string>> Create(CreateRoomDto request, CancellationToken cancellationToken);
    public Task<Result<List<Room>>> GetAll(CancellationToken cancellationToken);
    public Task<Result<GetRoomDto>> GetById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(UpdateRoomDto request, CancellationToken cancellationToken);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    //Admin
    public Task<Result<List<GetRoomDto>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);
}
