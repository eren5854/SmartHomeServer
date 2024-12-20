using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.Middlewares;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.Users;

namespace SmartHomeServer.Services.Users;

public sealed class AppUserService(
    IAppUserRepository appUserRepository,
    IMapper mapper) : IAppUserService
{
    public async Task<Result<string>> Create(CreateAppUserDto request, CancellationToken cancellationToken)
    {
        AppUser appUser = mapper.Map<AppUser>(request);
        appUser.CreatedBy = request.UserName;
        appUser.CreatedDate = DateTime.Now;
        appUser.EmailConfirmed = true;
        appUser.IsActive = true;
        appUser.SecretToken = Generate.GenerateSecretKey();

        return await appUserRepository.Create(appUser, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateAppUserDto request, CancellationToken cancellationToken)
    {
        AppUser? appUser = appUserRepository.GetById(request.Id);
        if (appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı!");
        }

        mapper.Map(request, appUser);
        appUser.UpdatedBy = request.UserName;
        appUser.UpdatedDate = DateTime.Now;

        return await appUserRepository.Update(appUser, cancellationToken);
    }

    public async Task<Result<string>> UpdateSecretToken(Guid Id, CancellationToken cancellationToken)
    {
        AppUser? appUser = appUserRepository.GetById(Id);
        if (appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı!");
        }

        appUser.SecretToken = Generate.GenerateSecretKey();

        return await appUserRepository.Update(appUser, cancellationToken);
    }

    //Sadece admin için
    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await appUserRepository.DeleteById(Id, cancellationToken);
    }

    //Sadece Admin için
    public async Task<Result<List<AppUser>>> GetAll(CancellationToken cancellationToken)
    {
        return await appUserRepository.GetAll(cancellationToken);
    }

    //public static string GenerateApiKey()
    //{
    //    using (var hmac = new HMACSHA256())
    //    {
    //        var key = Convert.ToBase64String(hmac.Key);
    //        return key.Replace("+", "").Replace("/", "").Replace("=", "");
    //    }
    //}
}
