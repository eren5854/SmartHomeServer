using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.Rooms;

namespace SmartHomeServer.Services.Rooms;

public sealed class RoomService(
    IRoomRepository roomRepository,
    IMapper mapper) : IRoomService
{
    public async Task<Result<string>> Create(CreateRoomDto request, CancellationToken cancellationToken)
    {
        Room room = mapper.Map<Room>(request);
        room.CreatedBy = "Admin";
        room.CreatedDate = DateTime.Now;

        return await roomRepository.Create(room, cancellationToken);
    }

    public async Task<Result<List<GetRoomDto>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await roomRepository.GetAllByUserId(Id, cancellationToken);
    }

    public async Task<Result<GetRoomDto>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await roomRepository.GetById(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateRoomDto request, CancellationToken cancellationToken)
    {
        Room? room = roomRepository.GetById(request.Id);
        if (room is null)
        {
            return Result<string>.Failure("Oda bulunamadı");
        }

        mapper.Map(request, room);
        room.UpdatedDate = DateTime.Now;
        room.UpdatedBy = "Admin";

        return await roomRepository.Update(room, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await roomRepository.DeleteById(Id, cancellationToken);
    }

    //Admin
    public async Task<Result<List<Room>>> GetAll(CancellationToken cancellationToken)
    {
        return await roomRepository.GetAll(cancellationToken);
    }
}
