using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class SensorService(
    SensorRepository sensorRepository,
    AppUserRepository appUserRepository,
    IMapper mapper)
{
    //Tüm kullanıcılar için sensör oluşturan metod.
    public async Task<Result<string>> Create(CreateSensorDto request, CancellationToken cancellationToken)
    {
        Sensor sensor = mapper.Map<Sensor>(request);
        sensor.SerialNo = await sensorRepository.CreateSerialNo(sensor);
        sensor.CreatedBy = "Admin";
        sensor.CreatedDate = DateTime.Now;
        sensor.IsActive = true;
        return await sensorRepository.Create(sensor, cancellationToken);
    }

    //Admin için tüm sensörleri getiren metod.
    public async Task<Result<List<Sensor>>> GetAll(CancellationToken cancellationToken)
    {
        return await sensorRepository.GetAll(cancellationToken);
    }

    public async Task<Result<Sensor>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await sensorRepository.GetById(Id, cancellationToken);
    }

    public async Task<Result<List<GetAllSensorDataDto>>> GetAllSensorData(CancellationToken cancellationToken)
    {
        return await sensorRepository.GetAllSensorData(cancellationToken);
    }

    //Sadece ilgili kulllanıcılar için sensör silme metodu.
    public async Task<Result<string>> DeleteById(Guid Id, Guid AppUserId, CancellationToken cancellationToken)
    {
        AppUser? appUser = appUserRepository.GetById(Id);
        if (appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        if (appUser.Id != AppUserId)
        {
            return Result<string>.Failure("Bu işlem için yetkiniz yok!!");
        }
        return await sensorRepository.DeleteById(Id, cancellationToken);
    }

    //Sadece ilgili kullanıcılar için sensör bilgileri güncelleme metodu
    public async Task<Result<string>> Update(UpdateSensorDto request, CancellationToken cancellationToken)
    {
        AppUser? appUser = appUserRepository.GetById(request.AppUserId);
        if (appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        if(appUser.Id !=  request.AppUserId)
        {
            return Result<string>.Failure("Bu işlem için yetkiniz yok!!");
        }

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

    //Espler tarafından çalıştırılacak metod
    public async Task<Result<string>> UpdateSensorData(UpdateSensorDataDto request, CancellationToken cancellationToken)
    {
        Sensor? sensor = sensorRepository.GetBySerialNo(request.SerialNo);
        if (sensor is null)
        {
            return Result<string>.Failure("Sensor bulunamadı");
        }

        if(sensor.SerialNo != request.SerialNo)
        {
            return Result<string>.Failure("Seri numara hatalı!!");
        }

        mapper.Map(request, sensor);
        sensor.UpdatedBy = "Admin";
        sensor.UpdatedDate = DateTime.Now;

        return await sensorRepository.Update(sensor, cancellationToken);
    }
}
