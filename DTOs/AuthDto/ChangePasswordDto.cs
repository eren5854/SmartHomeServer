namespace SmartHomeServer.DTOs.AuthDto;

public sealed record ChangePasswordDto(
    Guid Id,
    string CurrentPassword,
    string NewPassword);
