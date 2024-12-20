using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.LightTimeLogs;

public interface ILightTimeLogService
{
    public Task<Result<List<LightTimeLog>>> GetAllBySensorIdDaily(Guid Id, CancellationToken cancellationToken);
    public Task<Result<double>> GetAllBySensorIdWeekly(Guid Id, CancellationToken cancellationToken);
    public Task<Result<Dictionary<DateTime, double>>> GetDailyTotalsBySensorIdWeekly(Guid Id, CancellationToken cancellationToken);
}
