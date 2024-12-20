using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.TemplateSettingDto;
using SmartHomeServer.Services.TemplateSettings;

namespace SmartHomeServer.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class TemplateSettingsController(
    ITemplateSettingService templateSettingService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await templateSettingService.GetByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateTemplateSettingDto request, CancellationToken cancellationToken)
    {
        var response = await templateSettingService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
