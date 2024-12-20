using ED.Result;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.Users;

public interface IAppUserService
{
    public Task<Result<string>> Create(CreateAppUserDto request, CancellationToken cancellationToken);

    public Task<Result<string>> Update(UpdateAppUserDto request, CancellationToken cancellationToken);

    public Task<Result<string>> UpdateSecretToken(Guid Id, CancellationToken cancellationToken);

    //Sadece admin için
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    //Sadece Admin için
    public Task<Result<List<AppUser>>> GetAll(CancellationToken cancellationToken);
}
