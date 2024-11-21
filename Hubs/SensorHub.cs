using Microsoft.AspNetCore.SignalR;
using SmartHomeServer.Context;
using SmartHomeServer.Models;
using System.Threading;

namespace SmartHomeServer.Hubs;

public sealed class SensorHub(
    ApplicationDbContext context) : Hub
{
    public static Dictionary<string, string> Sensors = new();

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
    }

    public async Task Connect(string serialNo, CancellationToken cancellationToken)
    {
        Sensors.Add(Context.ConnectionId, serialNo);
        Sensor? sensor = await context.Sensors.FindAsync(serialNo);
        if (sensor is not null)
        {
            sensor.Status = "Healthy";
            await context.SaveChangesAsync(cancellationToken);
            await Clients.All.SendAsync("Sensors", sensor);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string serialNo;
        Sensors.TryGetValue(Context.ConnectionId, out serialNo);
        Sensor? sensor = await context.Sensors.FindAsync(serialNo);
        if (sensor is not null)
        {
            sensor.Status = "Unhealthy";
            await context.SaveChangesAsync();
            await Clients.All.SendAsync("Sensors", sensor);

        }
    }
}
