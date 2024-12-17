using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.RemoteControlKeyDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;

public sealed class RemoteControlKeysController(
    RemoteControlKeyService remoteControlKeyService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByRemoteControlSecretKey(string SecretKey, CancellationToken cancellationToken)
    {
        var response = await remoteControlKeyService.GetAllByRemoteControlSecretKey(SecretKey, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByRemoteControlId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await remoteControlKeyService.GetAllByRemoteControlId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRemoteControlKeyDto request, CancellationToken cancellationToken)
    {
        var response = await remoteControlKeyService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
