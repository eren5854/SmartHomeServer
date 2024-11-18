using SmartHomeServer.DTOs.AuthDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services;

public interface IJwtProvider
{
    Task<LoginResponseDto> CreateToken(AppUser user);
}
