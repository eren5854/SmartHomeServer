using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeServer.Abstractions;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.Services.Rooms;

namespace SmartHomeServer.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class RoomsController(
    IRoomService roomService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto request, CancellationToken cancellationToken)
    {
        var response = await roomService.Create(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await roomService.GetAllByUserId(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellation)
    {
        var response = await roomService.GetById(Id, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRoomDto request, CancellationToken cancellationToken)
    {
        var response = await roomService.Update(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await roomService.DeleteById(Id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }



    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await roomService.GetAll(cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
