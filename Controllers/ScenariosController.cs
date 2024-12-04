using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;

public sealed class ScenariosController(
    ScenarioService scenarioService) : ApiController
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
}
