using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Users;

public interface IAppUserRepository
{
    public Task<Result<string>> Create(AppUser appUser, CancellationToken cancellationToken);

    public Task<Result<List<AppUser>>> GetAll(CancellationToken cancellationToken);

    public Task<Result<AppUser>> GetById(Guid Id, CancellationToken cancellationToken);

    public Task<Result<AppUser>> GetBySecretToken(string SecretToken, CancellationToken cancellation);

    public AppUser? GetById(Guid Id);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(AppUser appUser, CancellationToken cancellationToken);
}
