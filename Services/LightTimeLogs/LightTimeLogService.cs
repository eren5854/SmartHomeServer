using AutoMapper;
using ED.Result;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.LightTimeLogs;

namespace SmartHomeServer.Services.LightTimeLogs;

public sealed class LightTimeLogService(
    ILightTimeLogRepository lightTimeLogRepository) : ILightTimeLogService
{
    public async Task<Result<List<LightTimeLog>>> GetAllBySensorIdDaily(Guid Id, CancellationToken cancellationToken)
    {
        return await lightTimeLogRepository.GetAllBySensorIdDaily(Id, cancellationToken);
    }

    public async Task<Result<double>> GetAllBySensorIdWeekly(Guid Id, CancellationToken cancellationToken)
    {
        return await lightTimeLogRepository.GetAllBySensorIdWeekly(Id, cancellationToken);
    }

    public async Task<Result<Dictionary<DateTime, double>>> GetDailyTotalsBySensorIdWeekly(Guid Id, CancellationToken cancellationToken)
    {
        return await lightTimeLogRepository.GetDailyTotalsBySensorIdWeekly(Id, cancellationToken);
    }
}
