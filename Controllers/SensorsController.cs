using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.BackgroundServices;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class SensorsController(
    SensorService sensorService) : ApiController
{
    //Users
    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSensorByUserId(Guid Id, CancellationToken cancellation)
    {
        var response = await sensorService.GetAllSensorByUserId(Id, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSensorDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSecretKeyById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await sensorService.UpdateSecretKeyById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await sensorService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await sensorService.GetById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }



    //Esp
    [HttpGet]
    public async Task<IActionResult> GetBySecretKey(string SecretKey, CancellationToken cancellationToken)
    {
        var response = await sensorService.GetBySecretKey(SecretKey, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSensorData(UpdateSensorDataDto request, CancellationToken cancellationToken)
    {
        var response = await sensorService.UpdateSensorData(request, cancellationToken);
        //BackgroundJob.Enqueue(() => TimeBackgroundService.Test());
        return StatusCode(response.StatusCode, response);
    }



    //Admin
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await sensorService.GetAll(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllSensorData(CancellationToken cancellationToken)
    {
        var response = await sensorService.GetAllSensorData(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

}
