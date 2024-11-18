namespace SmartHomeServer.DTOs.AuthDto;

public sealed record ForgotPasswordDto(
    string Email,
    int ForgotPasswordCode);
