using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class TemplateSettingRepository(
    ApplicationDbContext context)
{
    public void CreateDefaultTemplate(Guid Id)
    {
        TemplateSetting? templateSetting = context.TemplateSettings.Where(p => p.AppUserId == Id).FirstOrDefault();
        if(templateSetting is null)
        {
            templateSetting = new()
            {
                ContainerLayout = "wide",
                HeaderBg = "color_1",
                HeaderPosition = "fixed",
                Layout = "vertical",
                NavHeaderBg = "color_1",
                Primary = "color_1",
                SidebarBg = "color_1",
                SidebarPosition = "fixed",
                SidebarStyle = "full",
                Typography = "poppins",
                Version = "dark",
                AppUserId = Id,
                CreatedBy = "User",
                CreatedDate = DateTime.Now
            };

            context.Add(templateSetting);
            context.SaveChanges();
            //return Result<string>.Succeed("Şablon ayarları oluşturuldu");
        }

    }

    public async Task<Result<TemplateSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var templateSetting = await context.TemplateSettings.Where(p => p.AppUserId == Id).FirstOrDefaultAsync(cancellationToken);
        if(templateSetting is null)
        {
            return Result<TemplateSetting>.Failure("Şablon ayarı bulunamadı");
        }
        return Result<TemplateSetting>.Succeed(templateSetting);
    }

    public TemplateSetting? GetById(Guid Id)
    {
        return context.TemplateSettings.SingleOrDefault(p => p.Id == Id);
    }

    public async Task<Result<string>> Update(TemplateSetting templateSetting, CancellationToken cancellationToken)
    {
        context.Update(templateSetting);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Şablon ayarları güncellendi");
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        TemplateSetting? templateSetting = GetById(Id);
        if (templateSetting is null)
        {
            return Result<string>.Failure("Şablon ayarı bulunamadı");
        }

        templateSetting.IsDeleted = true;
        context.Update(templateSetting);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Şablon silindi");
    }
}
