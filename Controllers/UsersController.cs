using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.Services.Users;

namespace SmartHomeServer.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class UsersController(
    IAppUserService appUserService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Update(UpdateAppUserDto request, CancellationToken cancellationToken)
    {
        var response = await appUserService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSecretToken(Guid Id, CancellationToken cancellationToken)
    {
        var response = await appUserService.UpdateSecretToken(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await appUserService.GetAll(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await appUserService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
