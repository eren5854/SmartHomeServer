using AutoMapper;
using ED.Result;
using Microsoft.AspNetCore.Http.Metadata;
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

        //Scenario scenario = new()
        //{
        //    AppUserId = request.AppUserId,
        //    ScenarioName = request.ScenarioName,
        //    ScenarioDescription = request.ScenarioDescription,
        //    CreatedDate = DateTime.Now,
        //    CreatedBy = "Admin",
        //    Trigger = new()
        //    {
        //        SensorId = request.TriggerSensorId,
        //        TriggerType = request.TriggerType,
        //        TriggerValue = request.TriggerValue,
        //        TriggerTime = request.TriggerTime,
        //        Action = new()
        //        {
        //            SensorId = request.ActionSensorId,
        //            ActionType = request.ActionType,
        //            Value = request.ActionValue
        //        }
        //    }
        //};

        return await scenarioRepository.Create(scenario, cancellationToken);
    }

    public async Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await scenarioRepository.GetAllScenarioByUserId(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateScenarioDto request, CancellationToken cancellationToken)
    {
        Scenario? scenario = scenarioRepository.GetById(request.Id);
        if (scenario is null)
        {
            return Result<string>.Succeed("Senaryo bulunamadı");
        }

        mapper.Map(request, scenario);
        scenario.UpdatedDate = DateTime.Now;
        scenario.UpdatedBy = "Admin";

        return await scenarioRepository.Update(scenario, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await scenarioRepository.DeleteById(Id, cancellationToken);
    }
}
