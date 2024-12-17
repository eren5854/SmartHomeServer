using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class LightTimeLogsController(
    LightTimeLogService lightTimeLogService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllBySensorIdDaily(Guid Id, CancellationToken cancellationToken)
    {
        var response = await lightTimeLogService.GetAllBySensorIdDaily(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBySensorIdWeekly(Guid Id, CancellationToken cancellationToken)
    {
        var response = await lightTimeLogService.GetAllBySensorIdWeekly(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetDailyTotalsBySensorIdWeekly(Guid Id, CancellationToken cancellationToken)
    {
        var response = await lightTimeLogService.GetDailyTotalsBySensorIdWeekly(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
