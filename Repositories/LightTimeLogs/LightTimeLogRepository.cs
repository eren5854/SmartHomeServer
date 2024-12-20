using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.LightTimeLogs;

public sealed class LightTimeLogRepository(
    ApplicationDbContext context) : ILightTimeLogRepository
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
            .OrderBy(o => o.StartDate)
            .ToListAsync(cancellationToken);

        return Result<List<LightTimeLog>>.Succeed(lightTimeLogs);
    }

    public async Task<Result<double>> GetAllBySensorIdWeekly(Guid id, CancellationToken cancellationToken)
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

        double total = lightTimeLogs.Sum(log => log.TimeCount ?? 0);

        return Result<double>.Succeed(total);
    }

    public async Task<Result<Dictionary<DateTime, double>>> GetDailyTotalsBySensorIdWeekly(Guid id, CancellationToken cancellationToken)
    {
        var today = DateTime.Now.Date; // Bugünün tarihini al (saat kısmı 00:00:00 olacak şekilde)
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Haftanın başlangıcı (Pazartesi)
        var endOfWeek = startOfWeek.AddDays(6); // Haftanın sonu (Pazar)

        // Haftanın başından sonuna kadar olan verileri getir
        var lightTimeLogs = await context.LightTimeLogs
            .Where(p => p.SensorId == id &&
                        p.StartDate.HasValue &&
                        p.StartDate.Value.Date >= startOfWeek &&
                        p.StartDate.Value.Date <= endOfWeek)
            .ToListAsync(cancellationToken);

        // Her gün için toplam TimeCount hesapla
        var dailyTotals = lightTimeLogs
            .GroupBy(log => log.StartDate!.Value.Date) // Tarihe göre grupla
            .ToDictionary(
                group => group.Key, // Tarih
                group => group.Sum(log => log.TimeCount ?? 0) // O günün toplam TimeCount'u
            );

        // Haftanın her gününü (Pazartesi'den Pazar'a) ekle ve eksik günler için toplamı 0 yap
        for (var date = startOfWeek; date <= endOfWeek; date = date.AddDays(1))
        {
            if (!dailyTotals.ContainsKey(date))
            {
                dailyTotals[date] = 0;
            }
        }

        return Result<Dictionary<DateTime, double>>.Succeed(dailyTotals);
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
