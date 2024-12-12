using AutoMapper;
using ED.Result;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Enums;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;
using System.Threading;

namespace SmartHomeServer.Hubs;

public sealed class SensorHub(SensorRepository sensorRepository,
    LightTimeLogRepository lightTimeLogRepository,
    IMapper mapper) : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    //public static Dictionary<string, string> Sensors = new();

    //public async Task SendMessage(string message)
    //{
    //    await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
    //}

    //public async Task Connect(string serialNo, CancellationToken cancellationToken)
    //{
    //    Sensors.Add(Context.ConnectionId, serialNo);
    //    Sensor? sensor = await context.Sensors.FindAsync(serialNo);
    //    if (sensor is not null)
    //    {
    //        sensor.Status = "Healthy";
    //        await context.SaveChangesAsync(cancellationToken);
    //        await Clients.All.SendAsync("Sensors", sensor);
    //    }
    //}

    //public override async Task OnDisconnectedAsync(Exception? exception)
    //{
    //    string serialNo;
    //    Sensors.TryGetValue(Context.ConnectionId, out serialNo!);
    //    Sensor? sensor = await context.Sensors.FindAsync(serialNo);
    //    if (sensor is not null)
    //    {
    //        sensor.Status = "Unhealthy";
    //        await context.SaveChangesAsync();
    //        await Clients.All.SendAsync("Sensors", sensor);

    //    }
    //}
}
