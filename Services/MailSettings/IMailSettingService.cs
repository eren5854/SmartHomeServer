using ED.Result;
using SmartHomeServer.DTOs.MailSettingDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.MailSettings;

public interface IMailSettingService
{
    public Task<Result<MailSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(UpdateMailSettingDto request, CancellationToken cancellationToken);
}
