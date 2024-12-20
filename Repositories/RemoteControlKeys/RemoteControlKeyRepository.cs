using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.RemoteControlKeyDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.RemoteControlKeys;

public sealed class RemoteControlKeyRepository(
    ApplicationDbContext context) : IRemoteControlKeyRepository
{
    public async Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlSecretKey(string SecretKey, CancellationToken cancellationToken)
    {

        var remoteControlKeys = await context.RemoteControlKeys
            .Where(p => p.RemoteControl.SecretKey == SecretKey)
            .OrderBy(o => o.KeyQueue)
            .ToListAsync(cancellationToken);

        var remoteControlKeyDtos = remoteControlKeys
            .Select(key => new GetAllRemoteControlKeyDto(
                key.Id,
                key.KeyName,
                key.KeyCode,
                key.KeyValue,
                key.KeyQueue))
           .ToList();

        return Result<List<GetAllRemoteControlKeyDto>>.Succeed(remoteControlKeyDtos);
    }

    public async Task<Result<List<GetAllRemoteControlKeyDto>>> GetAllByRemoteControlId(Guid Id, CancellationToken cancellationToken)
    {

        var remoteControlKeys = await context.RemoteControlKeys
            .Where(p => p.RemoteControl.Id == Id)
            .OrderBy(o => o.KeyQueue)
            .ToListAsync(cancellationToken);

        var remoteControlKeyDtos = remoteControlKeys
            .Select(key => new GetAllRemoteControlKeyDto(
                key.Id,
                key.KeyName,
                key.KeyCode,
                key.KeyValue,
                key.KeyQueue))
           .ToList();

        return Result<List<GetAllRemoteControlKeyDto>>.Succeed(remoteControlKeyDtos);
    }
    public async Task<Result<string>> Update(RemoteControlKey remoteControlKey, CancellationToken cancellationToken)
    {
        context.Update(remoteControlKey);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Güncelleme başarılı");
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        RemoteControlKey? remoteControlKey = GetById(Id);
        if (remoteControlKey is null)
        {
            return Result<string>.Failure("Kayıt bulunamadı");
        }

        remoteControlKey.IsDeleted = true;
        context.Update(remoteControlKey);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Silme işmei başarılı");
    }

    public RemoteControlKey? GetById(Guid Id)
    {
        return context.RemoteControlKeys.SingleOrDefault(s => s.Id == Id);
    }

    public async void AddKey(Guid Id)
    {
        // Diziyi tanımla
        List<RemoteControlKey> keyList = new();

        // Kumanda tuş isimlerini belirle
        string[] keyNames = { "Power", "Input", "Settings", "Menu", "Numeric", "Back",
                          "Channel Up", "Channel Down", "Home", "Volume Up", "Volume Down", "Up", "Left", "Ok", "Right", "Down", "Empty", "Mute" };


        // Tuş isimlerini kullanarak nesneleri oluştur ve diziye ekle
        for (int i = 0; i < 18; i++)
        {
            RemoteControlKey remoteControlKey = new()
            {
                KeyName = keyNames[i],  // Tuş ismini ayarla
                KeyCode = "",          // Boş string olarak ayarla
                KeyValue = false,       // False olarak ayarla
                KeyQueue = i,
                RemoteControlId = Id
            };

            keyList.Add(remoteControlKey);
            await context.AddAsync(remoteControlKey);
            //await context.SaveChangesAsync();
        }
    }
}
