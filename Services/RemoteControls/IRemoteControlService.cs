using ED.Result;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.RemoteControls;

public interface IRemoteControlService
{
    public Task<Result<string>> Create(CreateRemoteControlDto request, CancellationToken cancellationToken);

    public Task<Result<List<RemoteControl>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<RemoteControl>> GetById(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(UpdateRemoteControlDto request, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
}
