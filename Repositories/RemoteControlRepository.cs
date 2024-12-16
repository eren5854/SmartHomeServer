using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class RemoteControlRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(RemoteControl remoteControl, CancellationToken cancellationToken)
    {
        await context.AddAsync(remoteControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kontrolcü kaydı başarılı");
    }

    public async Task<Result<List<RemoteControl>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var remoteControls = await context.RemoteControls.Where(p => p.AppUserId == Id).ToListAsync(cancellationToken);
        return Result<List<RemoteControl>>.Succeed(remoteControls);
    }

    public async Task<Result<string>> Update(RemoteControl remoteControl, CancellationToken cancellationToken)
    {
        context.Update(remoteControl);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kontrolcü güncellemesi başarılı");
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        RemoteControl? remoteControl = GetById(Id);
        if (remoteControl is null)
        {
            return Result<string>.Failure("Kontrolcü bulunamadı");

        }

        remoteControl.IsDeleted = true;
        context.Update(remoteControl);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kontrolcü silme başarılı");
    }

    public RemoteControl? GetById(Guid Id)
    {
        return context.RemoteControls.SingleOrDefault(s => s.Id == Id);
    }

    public async Task<Result<GetRemoteControlDataDto>> GetRemoteControlDataBySecretKey(string SecretKey, CancellationToken cancellationToken)
    {
        GetRemoteControlDataDto? remoteControl = await context.RemoteControls
            .Where(p => p.SecretKey == SecretKey)
            .Select(s => new GetRemoteControlDataDto(
                s.SerialNo,
                s.SecretKey!,
                s.OnOff,
                s.NextChannel,
                s.PrevChannel,
                s.VolumeUp,
                s.VolumeDown,
                s.ChannelMenu,
                s.Source)).FirstOrDefaultAsync();
        if (remoteControl is null)
        {
            return Result<GetRemoteControlDataDto>.Failure("Kontrolcü bulunamadı");

        }
        return Result<GetRemoteControlDataDto>.Succeed(remoteControl);
    }
}
