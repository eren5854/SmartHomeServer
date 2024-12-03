using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHomeServer.DTOs.AuthDto;
using SmartHomeServer.Models;
using SmartHomeServer.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHomeServer.Services;

public class JwtProvider(
    UserManager<AppUser> userManager,
    IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    public async Task<LoginResponseDto> CreateToken(AppUser user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FullName", user.FullName!),
            new Claim("Email", user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            //new Claim("UserRole", user.UserRole.ToString())
        };

        DateTime expires = DateTime.Now.AddMonths(3);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        string refreshToken = Guid.NewGuid().ToString();
        DateTime refreshTokenExpires = expires;

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshTokenExpires);
    }
}
