using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class ScenarioService(
    ScenarioRepository scenarioRepository,
    IMapper mapper)
{ 
    public async Task<Result<string>> Create(CreateScenarioDto request, CancellationToken cancellationToken)
    {
        Scenario scenario = mapper.Map<Scenario>(request);
        scenario.CreatedDate = DateTime.Now;
        scenario.CreatedBy = "Admin";

        return await scenarioRepository.Create(scenario, cancellationToken);
    }

    public async Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await scenarioRepository.GetAllScenarioByUserId(Id, cancellationToken);
    }
}
