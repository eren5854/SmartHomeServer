using ED.Result;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.Scenarios;

public interface IScenarioService
{
    public Task<Result<string>> Create(CreateScenarioDto request, CancellationToken cancellationToken);
    public Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<Scenario>> GetById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(UpdateScenarioDto request, CancellationToken cancellationToken);
    public Task<Result<string>> UpdateIsActive(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
}
