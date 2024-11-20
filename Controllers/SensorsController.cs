using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class SensorsController(
    SensorService sensorService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await sensorService.GetAll(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSensorData(CancellationToken cancellationToken)
    {
        var response = await sensorService.GetAllSensorData(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await sensorService.GetById(Id,cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSensorDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSensorData(UpdateSensorDataDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.UpdateSensorData(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, Guid AppUserId, CancellationToken cancellationToken)
    {
        var response = await sensorService.DeleteById(Id, AppUserId, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
