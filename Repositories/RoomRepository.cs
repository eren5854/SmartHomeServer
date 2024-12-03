using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class RoomRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(Room room, CancellationToken cancellationToken)
    {
        await context.AddAsync(room, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Oda kaydı başarılı");
    }

    public async Task<Result<List<Room>>> GetAll(CancellationToken cancellationToken)
    {
        var rooms = await context.Rooms.ToListAsync(cancellationToken);
        return Result<List<Room>>.Succeed(rooms);
    }

    public Room? GetById(Guid Id)
    {
        return context.Rooms.SingleOrDefault(s => s.Id == Id);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        Room? room = GetById(Id);
        if (room is null)
        {
            return Result<string>.Failure("Oda bulunamadı");
        }

        room.IsDeleted = true;
        context.Update(room);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Oda silme işlemi başarılı");
    }

    public async Task<Result<string>> Update(Room room, CancellationToken cancellationToken)
    {
        context.Update(room);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Oda güncelleme işlemi başarılı");
    }
}
