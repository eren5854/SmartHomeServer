using AutoMapper;
using ED.Result;
using Microsoft.AspNetCore.SignalR;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Enums;
using SmartHomeServer.Hubs;
using SmartHomeServer.Middlewares;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class SensorService(
    SensorRepository sensorRepository,
    LightTimeLogRepository lightTimeLogRepository,
    IHubContext<SensorHub> hubContext,
    IMapper mapper)
{
    //Tüm kullanıcılar için yetkili olarak erişebilecekleri metodlar.
    public async Task<Result<string>> Create(CreateSensorDto request, CancellationToken cancellationToken)
    {
        Sensor sensor = mapper.Map<Sensor>(request);
        sensor.SerialNo = await sensorRepository.CreateSerialNo(sensor);
        sensor.SecretKey = Generate.GenerateSecretKey();
        sensor.CreatedBy = "Admin";
        sensor.CreatedDate = DateTime.Now;
        sensor.IsActive = true;
        return await sensorRepository.Create(sensor, cancellationToken);
    }

    public async Task<Result<List<Sensor>>> GetAllSensorByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await sensorRepository.GetAllSensorByUserId(Id, cancellationToken);
    }
    
    public async Task<Result<string>> Update(UpdateSensorDto request, CancellationToken cancellationToken)
    {
        //AppUser? appUser = appUserRepository.GetById(request.AppUserId);
        //if (appUser is null)
        //{
        //    return Result<string>.Failure("Kullanıcı bulunamadı");
        //}

        //if(appUser.Id !=  request.AppUserId)
        //{
        //    return Result<string>.Failure("Bu işlem için yetkiniz yok!!");
        //}

        Sensor? sensor = sensorRepository.GetById(request.Id);
        if (sensor is null)
        {
            return Result<string>.Failure("Sensor bulunamadı");
        }

        if (sensor.SensorType != request.SensorType)
        {
            sensor.SensorType = request.SensorType;
            sensor.SerialNo = await sensorRepository.CreateSerialNo(sensor);
        }

        mapper.Map(request, sensor);
        sensor.UpdatedBy = "Admin";
        sensor.UpdatedDate = DateTime.Now;

        return await sensorRepository.Update(sensor, cancellationToken);
    }

    public async Task<Result<string>> UpdateSecretKeyById(Guid Id, CancellationToken cancellationToken)
    {
        Sensor? sensor = sensorRepository.GetById(Id);
        if (sensor is null)
        {
            return Result<string>.Failure("Sensor bulunamadı");
        }

        sensor.SecretKey = Generate.GenerateSecretKey();
        sensor.UpdatedBy = "Admin";
        sensor.UpdatedDate = DateTime.Now;
        return await sensorRepository.Update(sensor, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        //AppUser? appUser = appUserRepository.GetById(Id);
        //if (appUser is null)
        //{
        //    return Result<string>.Failure("Kullanıcı bulunamadı");
        //}

        //if (appUser.Id != AppUserId)
        //{
        //    return Result<string>.Failure("Bu işlem için yetkiniz yok!!");
        //}
        return await sensorRepository.DeleteById(Id, cancellationToken);
    }
    
    public async Task<Result<Sensor>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await sensorRepository.GetById(Id, cancellationToken);
    }
    


    //Espler tarafından çalıştırılacak metodlar
    public async Task<Result<GetAllSensorDataDto>> GetBySecretKey(string SecretKey, CancellationToken cancellationToken)
    {
        return await sensorRepository.GetBySecretKey(SecretKey, cancellationToken);
    }

    public async Task<Result<string>> UpdateSensorData(UpdateSensorDataDto request, CancellationToken cancellationToken)
    {
        Sensor? sensor = sensorRepository.GetBySecretKey(request.SecretKey);
        if (sensor is null)
        {
            return Result<string>.Failure("Sensor bulunamadı");
        }

        if(sensor.SecretKey != request.SecretKey)
        {
            return Result<string>.Failure("Secret Key hatalı!!");
        }

        if (request.SensorType == SensorTypeEnum.Light)
        {
            if (request.Data1 == 1 && sensor.Data1 == 0)
            {
                LightTimeLog lightTimeLog = new()
                {
                    SensorId = sensor.Id,
                    StartDate = DateTime.Now,
                    OpenCount = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin"
                };
                await lightTimeLogRepository.Create(lightTimeLog, cancellationToken);

            }
            
            else if(request.Data1 == 0 && sensor.Data1 == 1)
            {
                LightTimeLog? lightTimeLog = lightTimeLogRepository.GetBySensorId(sensor.Id);
                if (lightTimeLog is not null)
                {
                    lightTimeLog!.FinishDate = DateTime.Now;
                    if (lightTimeLog.StartDate.HasValue && lightTimeLog.FinishDate.HasValue)
                    {
                        lightTimeLog.TimeCount = (lightTimeLog.FinishDate.Value - lightTimeLog.StartDate.Value).TotalSeconds;
                    }
                    lightTimeLog.UpdatedDate = DateTime.Now;
                    lightTimeLog.UpdatedBy = "Admin";

                    await lightTimeLogRepository.Update(lightTimeLog, cancellationToken);
                }
            }

        }

        mapper.Map(request, sensor);
        sensor.UpdatedBy = "Admin";
        sensor.UpdatedDate = DateTime.Now;

        await hubContext.Clients.All.SendAsync("Sensor", sensor);

        return await sensorRepository.Update(sensor, cancellationToken);
    }



    //Admin için tüm sensörleri getiren metodlar.
    public async Task<Result<List<Sensor>>> GetAll(CancellationToken cancellationToken)
    {
        return await sensorRepository.GetAll(cancellationToken);
    }

    public async Task<Result<List<GetAllSensorDataDto>>> GetAllSensorData(CancellationToken cancellationToken)
    {
        return await sensorRepository.GetAllSensorData(cancellationToken);
    }

}