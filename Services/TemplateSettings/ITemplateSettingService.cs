using AutoMapper;
using ED.Result;
using SmartHomeServer.DTOs.TemplateSettingDto;
using SmartHomeServer.Models;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services.TemplateSettings;

public interface ITemplateSettingService
{
    public Task<Result<TemplateSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> Update(UpdateTemplateSettingDto request, CancellationToken cancellationToken);
}
