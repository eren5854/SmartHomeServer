namespace SmartHomeServer.DTOs.AuthDto;

public sealed record ChangePasswordUsingTokenDto(
    string Email,
    string NewPassword,
    string Token);
