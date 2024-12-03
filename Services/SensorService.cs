﻿using AutoMapper;
using ED.Result;
using Microsoft.AspNetCore.SignalR;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Hubs;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class SensorService(
    SensorRepository sensorRepository,
    IHubContext<SensorHub> hubContext,
    IMapper mapper)
{
    //Tüm kullanıcılar için yetkili olarak erişebilecekleri metodlar.
    public async Task<Result<string>> Create(CreateSensorDto request, CancellationToken cancellationToken)
    {
        Sensor sensor = mapper.Map<Sensor>(request);
        sensor.SerialNo = await sensorRepository.CreateSerialNo(sensor);
        sensor.SecretKey = sensorRepository.GenerateSecretKey();
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

        mapper.Map(request, sensor);
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
            return Result<string>.Failure("Seri numara hatalı!!");
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