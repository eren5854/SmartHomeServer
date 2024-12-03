using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.Enums;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class UsersController(
    AppUserService appUserService) : ApiController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppUserDto request, CancellationToken cancellationToken)
    {
        var response = await appUserService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAppUserDto request, CancellationToken cancellationToken)
    {
        var response = await appUserService.Update(request, cancellationToken);
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
