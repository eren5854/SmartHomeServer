using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SmartHomeServer.Models;
using System.Security.Claims;

namespace SmartHomeServer.Hubs;

//[Authorize]
public sealed class SensorHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendSensorData(Sensor sensorData)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userId, out var parsedUserId) && sensorData.AppUserId == parsedUserId)
        {
            await Clients.User(userId).SendAsync("ReceiveSensorData", sensorData);
        }
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnConnectedAsync();
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
