using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Middlewares;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class RemoteControlService(
    RemoteControlRepository remoteControlRepository,
    IMapper mapper)
{
    public async Task<Result<string>> Create(CreateRemoteControlDto request, CancellationToken cancellationToken)
    {
        RemoteControl remoteControl = mapper.Map<RemoteControl>(request);
        remoteControl.SerialNo = "";
        remoteControl.SecretKey = Generate.GenerateSecretKey();
        remoteControl.CreatedBy = "User";
        remoteControl.CreatedDate = DateTime.Now;

        return await remoteControlRepository.Create(remoteControl, cancellationToken);
    }

    public async Task<Result<List<RemoteControl>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlRepository.GetAllByUserId(Id, cancellationToken);
    }

    public async Task<Result<string>> Update()
}
