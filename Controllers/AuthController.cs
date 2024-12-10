using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.DTOs.AuthDto;
using SmartHomeServer.Services;

namespace SmartHomeServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(
    AuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
    {
        var response = await authService.Login(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Signup(CreateAppUserDto request, CancellationToken cancellationToken)
    {
        var response = await authService.Signup(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto request, CancellationToken cancellationToken)
    {
        var response = await authService.ChangePassword(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request, CancellationToken cancellationToken)
    {
        var response = await authService.ForgotPassword(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePasswordUsingToken(ChangePasswordUsingTokenDto request, CancellationToken cancellationToken)
    {
        var response = await authService.ChangePasswordUsingToken(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
