using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Scenarios;

public interface IScenarioRepository
{
    public Task<Result<string>> Create(Scenario scenario, CancellationToken cancellationToken);
    public Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<Scenario>> GetById(Guid Id, CancellationToken cancellationToken);
    public Scenario? GetById(Guid Id);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(Scenario scenario, CancellationToken cancellationToken);

}
