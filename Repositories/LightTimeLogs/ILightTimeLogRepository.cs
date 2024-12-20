using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.LightTimeLogs;

public interface ILightTimeLogRepository
{
    public Task<Result<string>> Create(LightTimeLog lightTimeLog, CancellationToken cancellationToken);
    public LightTimeLog? GetBySensorId(Guid id);
    public Task<Result<List<LightTimeLog>>> GetAllBySensorId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<List<LightTimeLog>>> GetAllBySensorIdDaily(Guid id, CancellationToken cancellationToken);
    public Task<Result<double>> GetAllBySensorIdWeekly(Guid id, CancellationToken cancellationToken);
    public Task<Result<Dictionary<DateTime, double>>> GetDailyTotalsBySensorIdWeekly(Guid id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(LightTimeLog lightTimeLog, CancellationToken cancellation);
    public Task<Result<string>> DeleteAll(CancellationToken cancellationToken);

}
