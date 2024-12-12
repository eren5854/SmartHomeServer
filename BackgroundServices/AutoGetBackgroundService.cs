using Microsoft.AspNetCore.SignalR;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Hubs;

namespace SmartHomeServer.BackgroundServices;

public class AutoGetBackgroundService(
    IHubContext<SensorHub> hubContext,
    ApplicationDbContext context)
{
    public void GetAllSensor()
    {
        var sensors = context.Sensors
           .Select(
               s => new GetAllSensorDataDto(
               s.SensorName,
               s.SerialNo,
               s.Status!,
               s.SensorType,
               s.Data1,
               s.Data2,
               s.Data3,
               s.Data4,
               s.Data5,
               s.Data6))
           .ToList();

        hubContext.Clients.All.SendAsync("Sensors", sensors);
    }
    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        await Task.Delay(1000);
    //        await GetSensors();
    //        //await GetSensors(2);
    //    }
    //}

    //private async Task GetSensors()
    //{
    //    var sensors = context.Sensors
    //       .Select(
    //           s => new GetAllSensorDataDto(
    //           s.SensorName,
    //           s.SerialNo,
    //           s.Status!,
    //           s.SensorType,
    //           s.Data1,
    //           s.Data2,
    //           s.Data3,
    //           s.Data4,
    //           s.Data5,
    //           s.Data6))
    //       .ToList();

    //    await hubContext.Clients.All.SendAsync("Sensors", sensors);
    //}
}
