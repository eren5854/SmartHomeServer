using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.TemplateSettingDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories.TemplateSettings;
using SmartHomeServer.Services.TemplateSettings;

namespace SmartHomeServer.Services;

public sealed class TemplateSettingService(
    ITemplateSettingRepository templateSettingRepository,
    IMapper mapper) : ITemplateSettingService
{
    public async Task<Result<TemplateSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        return await templateSettingRepository.GetByUserId(Id, cancellationToken);
    }

    public async Task<Result<string>> Update(UpdateTemplateSettingDto request, CancellationToken cancellationToken)
    {
        TemplateSetting? templateSetting = templateSettingRepository.GetById(request.Id);
        if (templateSetting is null)
        {
            return Result<string>.Failure("Şablon bulunamadı");
        }

        mapper.Map(request, templateSetting);
        templateSetting.UpdatedBy = "User";
        templateSetting.UpdatedDate = DateTime.Now;

        return await templateSettingRepository.Update(templateSetting, cancellationToken);
    }
}
