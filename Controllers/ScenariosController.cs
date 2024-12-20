using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.Services.Scenarios;

namespace SmartHomeServer.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class ScenariosController(
    IScenarioService scenarioService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateScenarioDto request, CancellationToken cancellationToken)
    {
        var response = await scenarioService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await scenarioService.GetAllScenarioByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await scenarioService.GetById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateScenarioDto request, CancellationToken cancellationToken)
    {
        var response = await scenarioService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateIsActive(Guid Id, CancellationToken cancellationToken)
    {
        var response = await scenarioService.UpdateIsActive(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await scenarioService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
