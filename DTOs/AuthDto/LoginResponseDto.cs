namespace SmartHomeServer.DTOs.AuthDto;

public sealed record LoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);
