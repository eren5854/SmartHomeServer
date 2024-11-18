using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class AppUserService(
    AppUserRepository appUserRepository,
    IMapper mapper)
{
    public async Task<Result<string>> Create(CreateAppUserDto request, CancellationToken cancellationToken)
    {
        AppUser appUser = mapper.Map<AppUser>(request);
        appUser.CreatedBy = request.UserName;
        appUser.CreatedDate = DateTime.Now;
        appUser.EmailConfirmed = true;
        appUser.IsActive = true;

        return await appUserRepository.Create(appUser, cancellationToken);
    }
}
