using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class NotificationRepository(
    ApplicationDbContext context)
{
    public async Task<Result<List<Notification>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var notifications = await context.Notifications
            .Where(p => p.AppUserId == Id)
            .OrderBy(o => o.CreatedDate)
            .ToListAsync(cancellationToken);
        return Result<List<Notification>>.Succeed(notifications);
    }

    public async Task<Result<string>> Update(Notification notification, CancellationToken cancellationToken)
    {
        context.Update(notification);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Güncelleme başarılı");
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        Notification? notification = GetById(Id);
        if (notification is null)
        {
            return Result<string>.Failure("Bildirim bulunamadı");
        }

        notification.IsDeleted = true;

        context.Update(notification);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Silme işlemi başarılı");
    }

    public Notification? GetById(Guid Id)
    {
        return context.Notifications.SingleOrDefault(s => s.Id == Id);
    }
}
