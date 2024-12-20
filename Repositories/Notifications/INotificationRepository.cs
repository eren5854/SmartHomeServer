using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Notifications;

public interface INotificationRepository
{
    public Task<Result<List<Notification>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(Notification notification, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);

    public Notification? GetById(Guid Id);
}
