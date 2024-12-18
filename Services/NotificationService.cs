﻿using ED.Result;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class NotificationService(
    NotificationRepository notificationRepository)
{
    public async Task<Result<List<Notification>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await notificationRepository.GetAllByUserId(Id, cancellationToken);
    }

    public async Task<Result<string>> UpdateIsActive(Guid Id ,CancellationToken cancellationToken)
    {
        Notification? notification = notificationRepository.GetById(Id);
        if (notification is null)
        {
            return Result<string>.Failure("Bildirim bulunamadı");
        }

        notification.IsActive = false;

        return await notificationRepository.Update(notification, cancellationToken);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        return await notificationRepository.DeleteById(Id, cancellationToken);
    }
}
