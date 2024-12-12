using AutoMapper;
using ED.Result;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class LightTimeLogService(
    LightTimeLogRepository lightTimeLogRepository)
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
        return await lightTimeLogRepository.GetDailyTotalsBySensorIdWeekly(Id,cancellationToken);
    }
}
