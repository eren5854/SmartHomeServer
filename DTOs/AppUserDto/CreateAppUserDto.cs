namespace SmartHomeServer.DTOs.AppUserDto;

public sealed record CreateAppUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password);
