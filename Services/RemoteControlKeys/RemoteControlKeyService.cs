using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.RemoteControlKeyDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.RemoteControlKeys;

namespace SmartHomeServer.Services.RemoteControlKeys;

public sealed class RemoteControlKeyService(
    IRemoteControlKeyRepository remoteControlKeyRepository,
    IMapper mapper) : IRemoteControlKeyService
{
    public async Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlSecretKey(string SecretKey, CancellationToken cancellationToken)
    {
        return await remoteControlKeyRepository.GetAllByRemoteControlSecretKey(SecretKey, cancellationToken);
    }

    public async Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlId(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlKeyRepository.GetAllByRemoteControlId(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateRemoteControlKeyDto request, CancellationToken cancellationToken)
    {
        RemoteControlKey? remoteControlKey = remoteControlKeyRepository.GetById(request.Id);
        if (remoteControlKey is null)
        {
            return Result<string>.Failure("Kayıt bulunamadı");
        }
        mapper.Map(request, remoteControlKey);
        remoteControlKey.UpdatedBy = "User";
        remoteControlKey.UpdatedDate = DateTime.Now;
        return await remoteControlKeyRepository.Update(remoteControlKey, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlKeyRepository.DeleteById(Id, cancellationToken);
    }
}
