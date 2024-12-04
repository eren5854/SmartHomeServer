using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.TriggerDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;

public sealed class TriggersController(
    TriggerService triggerService) : ApiController
{
    //[HttpPost]
    //public async Task<IActionResult> Crete(CreateTriggerDto request, CancellationToken cancellationToken)
    //{
    //    var response = await triggerService.Create(request, cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}

    //[HttpGet]
    //public async Task<IActionResult> GetAllTriggerByScenarioId(Guid Id, CancellationToken cancellationToken)
    //{
    //    var response = await triggerService.GetAllTriggerByScenarioId(Id, cancellationToken);
    //    return StatusCode(response.StatusCode, response);
    //}
}
