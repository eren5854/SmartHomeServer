using ED.GenericRepository;
using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Enums;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class SensorRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(Sensor sensor, CancellationToken cancellationToken)
    {
        await context.AddAsync(sensor, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Sensor kayıt işlemi başarılı");
    }

    public async Task<string> CreateSerialNo(Sensor sensor)
    {
        DateTime date = DateTime.Now;
        string formattedDate = date.ToString("ddMMyy");

        string type = sensor.SensorType switch
        {
            SensorTypeEnum.Relay => "RLY01",
            SensorTypeEnum.Temperature => "TMP01",
            SensorTypeEnum.Humidity => "HMD01",
            SensorTypeEnum.Ldr => "LDR01",
            SensorTypeEnum.Water => "WTR01",
            SensorTypeEnum.Pressure => "PRS01",
            SensorTypeEnum.Motion => "MTN01",
            SensorTypeEnum.Gas => "GAS01",
            SensorTypeEnum.Speed => "SPD01",
            _ => "OTH01"
        };

        var sensors = await context.Sensors
            .Where(p => p.CreatedDate.Date == date.Date && p.SensorType == sensor.SensorType)
            .ToListAsync();

        int number = sensors.Count + 1;

        return $"SN{type}{formattedDate}{number:D3}";
    }

    public async Task<Result<List<Sensor>>> GetAll(CancellationToken cancellationToken)
    {
        var sensors = await context.Sensors.OrderBy(o => o.CreatedDate).ToListAsync(cancellationToken);
        return Result<List<Sensor>>.Succeed(sensors);
    }

    public async Task<Result<List<GetAllSensorDataDto>>> GetAllSensorData(CancellationToken cancellationToken)
    {
        var sensors = await context.Sensors
            .Select(
                s => new GetAllSensorDataDto(
                s.SensorName,
                s.SerialNo,
                s.SensorType,
                s.Data1,
                s.Data2,
                s.Data3,
                s.Data4,
                s.Data5,
                s.Data6))
            .ToListAsync(cancellationToken);
        return Result<List<GetAllSensorDataDto>>.Succeed(sensors);
    }

    public async Task<Result<Sensor>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        Sensor? sensor = await context.Sensors.Where(p => p.Id == Id).FirstOrDefaultAsync(cancellationToken);
        if (sensor is null)
        {
            return Result<Sensor>.Failure("Sensor bulunamadı");
        }
        return Result<Sensor>.Succeed(sensor);
    }

    public Sensor? GetById(Guid Id)
    {
        return context.Sensors.SingleOrDefault(s => s.Id == Id);
    }

    public Sensor? GetBySerialNo(string SerialNo)
    {
        return context.Sensors.SingleOrDefault(s => s.SerialNo == SerialNo);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        Sensor? sensor = GetById(Id);
        if (sensor is null)
        {
            return Result<string>.Failure("Sensor bulunamadı");
        }

        sensor.IsDeleted = true;
        context.Update(sensor);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Sensor silme işlemi başarılı");
    }

    public async Task<Result<string>> Update(Sensor sensor, CancellationToken cancellationToken)
    {
        context.Update(sensor);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Sensor güncelleme işlemi başarılı");
    }
}
