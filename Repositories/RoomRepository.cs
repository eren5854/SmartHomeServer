﻿using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Models;
using System.Diagnostics;

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

    public async Task<Result<List<Room>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var rooms = await context.Rooms.Where(p => p.AppUserId == Id).ToListAsync(cancellationToken);
        return Result<List<Room>>.Succeed(rooms);
    }

    public async Task<Result<GetRoomDto>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        Room? room = await context.Rooms
            .Where(p => p.Id == Id)
            .Include(i => i.Sensors)
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
        {
            return Result<GetRoomDto>.Failure("Oda bulunamadı");
        }

        var roomDto = new GetRoomDto(
         room.Id,
         room.RoomName,
         room.RoomDescription!,
         new List<GetAllSensorDto>(
             room.Sensors!.Select(sensor => new GetAllSensorDto(
                 sensor.Id,
                 sensor.SensorName,
                 sensor.SerialNo,
                 sensor.Status ?? string.Empty,
                 sensor.SensorType,
                 sensor.Data1,
                 sensor.Data2,
                 sensor.Data3,
                 sensor.Data4,
                 sensor.Data5,
                 sensor.Data6
             ))
         )
     );

        return Result<GetRoomDto>.Succeed(roomDto);
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
    
    public Room? GetById(Guid Id)
    {
        return context.Rooms.SingleOrDefault(s => s.Id == Id);
    }
    
    public async Task<Result<string>> Update(Room room, CancellationToken cancellationToken)
    {
        context.Update(room);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Oda güncelleme işlemi başarılı");
    }
    
    
    
    //Admin
    public async Task<Result<List<Room>>> GetAll(CancellationToken cancellationToken)
    {
        var rooms = await context.Rooms.ToListAsync(cancellationToken);
        return Result<List<Room>>.Succeed(rooms);
    }




}
