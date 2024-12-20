using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.MailSettings;

public interface IMailSettingRepository
{
    public void CreateMailSetting(Guid Id);

    public Task<Result<MailSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(MailSetting mailSetting, CancellationToken cancellationToken);

    public MailSetting? GetById(Guid Id);
}
