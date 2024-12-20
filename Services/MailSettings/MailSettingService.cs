using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.MailSettingDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.MailSettings;

namespace SmartHomeServer.Services.MailSettings;

public sealed class MailSettingService(
    IMailSettingRepository mailSettingRepository,
    IMapper mapper) : IMailSettingService
{
    public async Task<Result<MailSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await mailSettingRepository.GetByUserId(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateMailSettingDto request, CancellationToken cancellationToken)
    {
        MailSetting? mailSetting = mailSettingRepository.GetById(request.Id);
        if (mailSetting is null)
        {
            return Result<string>.Failure("Mail ayarları bulunamadı");
        }

        mapper.Map(request, mailSetting);
        mailSetting.UpdatedBy = "User";
        mailSetting.UpdatedDate = DateTime.Now;

        return await mailSettingRepository.Update(mailSetting, cancellationToken);
    }
}
