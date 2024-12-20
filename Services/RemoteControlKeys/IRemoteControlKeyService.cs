using ED.Result;
using SmartHomeServer.DTOs.RemoteControlKeyDto;

namespace SmartHomeServer.Services.RemoteControlKeys;

public interface IRemoteControlKeyService
{
    public Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlSecretKey(string SecretKey, CancellationToken cancellationToken);

    public Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(UpdateRemoteControlKeyDto request, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
}
