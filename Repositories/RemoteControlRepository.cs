using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.Enums;
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
        var remoteControls = await context.RemoteControls
        .Where(p => p.AppUserId == Id)
        .Include(i => i.RemoteControlKeys)
        .OrderBy(o => o.CreatedDate)
        .ToListAsync(cancellationToken);

        // RemoteControlKeys'i KeyQueue'ya göre sıralıyoruz
        remoteControls.ForEach(remoteControl =>
        {
            remoteControl.RemoteControlKeys = remoteControl.RemoteControlKeys!
                .OrderBy(k => k.KeyQueue)
                .ToList();
        });

        return Result<List<RemoteControl>>.Succeed(remoteControls);
    }

    public async Task<Result<RemoteControl>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        RemoteControl? remoteControl = await context.RemoteControls
            .Where(p => p.Id == Id)
            .Include(i => i.RemoteControlKeys)
            .FirstOrDefaultAsync(cancellationToken);

        if (remoteControl == null)
        {
            return Result<RemoteControl>.Failure("Kayıt bulunamadı");
        }

        remoteControl.RemoteControlKeys = remoteControl.RemoteControlKeys!
                .OrderBy(k => k.KeyQueue)
                .ToList();
        return Result<RemoteControl>.Succeed(remoteControl);
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

    public async Task<string> CreateSerialNo()
    {
        DateTime today = DateTime.Today; // Sadece bugünün tarihi (saat 00:00)
        string formattedDate = today.ToString("ddMMyy");
        int number = 1;

        // Sadece bugünkü kayıtları al
        var remoteControls = await context.RemoteControls
            .Where(p => p.CreatedDate.Date == today) // Tarih eşleştirme
            .ToListAsync();

        if (remoteControls.Any()) // Liste boş değilse
        {
            number = remoteControls.Count + 1;
        }

        // Seri numarasını oluştur ve döndür
        return $"SNRCTV{formattedDate}{number:D3}";
    }

}
