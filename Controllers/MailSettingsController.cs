using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.MailSettingDto;
using SmartHomeServer.Services.MailSettings;

namespace SmartHomeServer.Controllers;

public sealed class MailSettingsController(
    IMailSettingService mailSettingService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await mailSettingService.GetByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateMailSettingDto request, CancellationToken cancellationToken)
    {
        var response = await mailSettingService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
