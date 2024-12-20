using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.Services.Notifications;

namespace SmartHomeServer.Controllers;

public sealed class NotificationsController(
    INotificationService notificationService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await notificationService.GetAllByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateIsActive(Guid Id, CancellationToken cancellationToken)
    {
        var response = await notificationService.UpdateIsActive(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await notificationService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
