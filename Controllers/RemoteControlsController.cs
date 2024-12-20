using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Services.RemoteControls;

namespace SmartHomeServer.Controllers;

public sealed class RemoteControlsController(
    IRemoteControlService remoteControlService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateRemoteControlDto request, CancellationToken cancellationToken)
    {
        var response = await remoteControlService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await remoteControlService.GetAllByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await remoteControlService.GetById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRemoteControlDto request, CancellationToken cancellationToken)
    {
        var response = await remoteControlService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await remoteControlService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
