using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.TriggerDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class TriggerService(
    TriggerRepository triggerRepository,
    IMapper mapper)
{
    //public async Task<Result<string>> Create(CreateTriggerDto request, CancellationToken cancellationToken)
    //{
    //    Trigger trigger = mapper.Map<Trigger>(request);
    //    trigger.CreatedDate = DateTime.Now;
    //    trigger.CreatedBy = "Admin";

    //    return await triggerRepository.Create(trigger, cancellationToken);
    //}

    //public async Task<Result<List<Trigger>>> GetAllTriggerByScenarioId(Guid Id, CancellationToken cancellationToken)
    //{
    //    return await triggerRepository.GetAllTriggerByScenarioId(Id, cancellationToken);
    //}
}
