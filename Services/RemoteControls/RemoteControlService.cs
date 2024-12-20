using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Middlewares;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.RemoteControlKeys;
using SmartHomeServer.Repositories.RemoteControls;

namespace SmartHomeServer.Services.RemoteControls;

public sealed class RemoteControlService(
    IRemoteControlRepository remoteControlRepository,
    IRemoteControlKeyRepository remoteControlKeyRepository,
    IMapper mapper) : IRemoteControlService
{
    public async Task<Result<string>> Create(CreateRemoteControlDto request, CancellationToken cancellationToken)
    {
        RemoteControl remoteControl = mapper.Map<RemoteControl>(request);
        remoteControl.SerialNo = await remoteControlRepository.CreateSerialNo();
        remoteControl.SecretKey = Generate.GenerateSecretKey();
        remoteControl.CreatedBy = "User";
        remoteControl.CreatedDate = DateTime.Now;

        //if(remoteControl.RemoteControlKeys != null)
        //{
        //    foreach(var key in remoteControl.RemoteControlKeys)
        //    {
        //        key.RemoteControlId = remoteControl.Id;
        //        //await remoteControlKeyRepository.Create(key, cancellationToken);
        //    }
        //}

        remoteControlKeyRepository.AddKey(remoteControl.Id);


        return await remoteControlRepository.Create(remoteControl, cancellationToken);
    }

    public async Task<Result<List<RemoteControl>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlRepository.GetAllByUserId(Id, cancellationToken);
    }

    public async Task<Result<RemoteControl>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlRepository.GetById(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateRemoteControlDto request, CancellationToken cancellationToken)
    {
        RemoteControl? remoteControl = remoteControlRepository.GetById(request.Id);
        if (remoteControl is null)
        {
            return Result<string>.Failure("Kayıt bulunamadı");
        }

        mapper.Map(request, remoteControl);
        remoteControl.UpdatedBy = "User";
        remoteControl.UpdatedDate = DateTime.Now;

        return await remoteControlRepository.Update(remoteControl, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await remoteControlRepository.DeleteById(Id, cancellationToken);
    }

    //public async Task<Result<string>> Update(UpdateRemoteControlDto request, CancellationToken cancellationToken)
    //{
    //    RemoteControl? remoteControl = remoteControlRepository.GetById(request.Id);
    //    if(remoteControl is null)
    //    {
    //        return Result<string>.Failure("Kayıt bulunamadı");
    //    }

    //    mapper.Map(request, remoteControl);
    //    remoteControl.UpdatedBy = "User";
    //    remoteControl.UpdatedDate = DateTime.Now;

    //    if (request.UpdateRemoteControlKeys != null)
    //    {
    //        var existingKeys = remoteControl.RemoteControlKeys.ToList();

    //        var updatedKeys = request.UpdateRemoteControlKeys;

    //        var keysToRemove = existingKeys
    //            .Where(existing => updatedKeys.All(updated => updated.Id != existing.Id))
    //            .ToList();

    //        foreach (var updatedKey in updatedKeys)
    //        {
    //            if (updatedKey.Id == Guid.Empty)
    //            {
    //                var newKey = mapper.Map<RemoteControlKey>(updatedKey);
    //                newKey.RemoteControlId = remoteControl.Id;
    //                remoteControl.RemoteControlKeys.Add(newKey);
    //            }
    //            else
    //            {
    //                var existingKey = existingKeys.FirstOrDefault(k => k.Id == updatedKey.Id);
    //                if (existingKey != null)
    //                {
    //                    mapper.Map(updatedKey, existingKey);
    //                }
    //            }
    //        }

    //        foreach (var keyToRemove in keysToRemove)
    //        {
    //            remoteControl.RemoteControlKeys.Remove(keyToRemove);
    //            //await remoteControlKeyRepository.DeleteAsync(keyToRemove.Id, cancellationToken);
    //        }
    //    }
    //    return await remoteControlRepository.Update(remoteControl, cancellationToken);
    //}
}
