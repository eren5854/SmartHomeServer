namespace SmartHomeServer.DTOs.AppUserDto;

public sealed record UpdateAppUserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName);
