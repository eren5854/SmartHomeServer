using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;
using System.Threading;

namespace SmartHomeServer.Repositories;

public sealed class LightTimeLogRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(LightTimeLog lightTimeLog, CancellationToken cancellationToken)
    {
        await context.AddAsync(lightTimeLog, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kayıt başarılı");
    }

    public LightTimeLog? GetBySensorId(Guid id)
    {
        return context.LightTimeLogs
            .Where(s => s.SensorId == id)
            .OrderByDescending(o => o.CreatedDate)
            .FirstOrDefault();
    }

    public async Task<Result<List<LightTimeLog>>> GetAllBySensorId(Guid Id, CancellationToken cancellationToken)
    {
        var lightTimeLogs = await context.LightTimeLogs
            .Where(p => p.SensorId == Id)
            .OrderByDescending(o => o.CreatedDate)
            .ToListAsync(cancellationToken);

        return Result<List<LightTimeLog>>.Succeed(lightTimeLogs);
    }

    public async Task<Result<List<LightTimeLog>>> GetAllBySensorIdDaily(Guid id, CancellationToken cancellationToken)
    {
        var today = DateTime.Now.Date; // Bugünün tarihini al (saat kısmı 00:00:00 olacak şekilde)

        var lightTimeLogs = await context.LightTimeLogs
            .Where(p => p.SensorId == id &&
                        p.StartDate.HasValue &&
                        p.StartDate.Value.Date == today)
            .ToListAsync(cancellationToken);

        return Result<List<LightTimeLog>>.Succeed(lightTimeLogs);
    }

    public async Task<Result<List<LightTimeLog>>> GetAllBySensorIdWeekly(Guid id, CancellationToken cancellationToken)
    {
        var today = DateTime.Now.Date; // Bugünün tarihini al (saat kısmı 00:00:00 olacak şekilde)
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Haftanın başlangıcı (Pazartesi)
        var endOfWeek = startOfWeek.AddDays(6); // Haftanın sonu (Pazar)

        var lightTimeLogs = await context.LightTimeLogs
            .Where(p => p.SensorId == id &&
                        p.StartDate.HasValue &&
                        p.StartDate.Value.Date >= startOfWeek &&
                        p.StartDate.Value.Date <= endOfWeek)
            .ToListAsync(cancellationToken);

        return Result<List<LightTimeLog>>.Succeed(lightTimeLogs);
    }


    //public async Task<Result<List<LightTimeLog>>> GetAllBySensorIdMonthly(Guid Id, CancellationToken cancellationToken)
    //{
    //    var month = 
    //}

    public async Task<Result<string>> Update(LightTimeLog lightTimeLog, CancellationToken cancellation)
    {
        context.Update(lightTimeLog);
        await context.SaveChangesAsync(cancellation);
        return Result<string>.Succeed("Güncelleme başarılı");
    }

    public async Task<Result<string>> DeleteAll(CancellationToken cancellationToken)
    {
        var lightTimeLogs = await context.LightTimeLogs.ToListAsync(cancellationToken);
        if (lightTimeLogs.Count == 0)
        {
            return Result<string>.Failure("Tablo boş");
        }

        foreach (var item in lightTimeLogs)
        {
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);  
        }

        return Result<string>.Succeed("Tüm liste temzilendi");
    }
}
