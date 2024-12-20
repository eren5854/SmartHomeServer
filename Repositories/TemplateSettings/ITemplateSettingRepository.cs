using ED.Result;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.TemplateSettings;

public interface ITemplateSettingRepository
{
    public void CreateDefaultTemplate(Guid Id);
    public Task<Result<TemplateSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken);
    public TemplateSetting? GetById(Guid Id);
    public Task<Result<string>> Update(TemplateSetting templateSetting, CancellationToken cancellationToken);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
}
