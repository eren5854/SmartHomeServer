using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.Notifications;

public interface INotificationService
{
    public Task<Result<List<Notification>>> GetAllByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> UpdateIsActive(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
}
