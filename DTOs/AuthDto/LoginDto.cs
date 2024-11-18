namespace SmartHomeServer.DTOs.AuthDto;

public sealed record LoginDto(
    string EmailOrUserName,
    string Password);
