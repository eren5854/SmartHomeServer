using ED.Result;
using SmartHomeServer.DTOs.RemoteControlKeyDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.RemoteControlKeys;

public interface IRemoteControlKeyRepository
{
    public Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlSecretKey(string SecretKey, CancellationToken cancellationToken);

    public Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(RemoteControlKey remoteControlKey, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    public RemoteControlKey? GetById(Guid Id);

    public void AddKey(Guid Id);
}
