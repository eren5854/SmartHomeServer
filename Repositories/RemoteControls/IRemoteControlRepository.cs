using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.RemoteControls;

public interface IRemoteControlRepository
{
    public Task<Result<string>> Create(RemoteControl remoteControl, CancellationToken cancellationToken);

    public Task<Result<List<RemoteControl>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<RemoteControl>> GetById(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(RemoteControl remoteControl, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    public RemoteControl? GetById(Guid Id);

    public Task<string> CreateSerialNo();
}
