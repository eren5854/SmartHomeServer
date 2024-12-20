using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.MailSettings;

public sealed class MailSettingRepository(
    ApplicationDbContext context) : IMailSettingRepository
{
    public void CreateMailSetting(Guid Id)
    {
        MailSetting? mailSetting = context.MailSettings.Where(p => p.AppUserId == Id).FirstOrDefault();
        if (mailSetting is null)
        {
            mailSetting = new()
            {
                Email = "example",
                AppPassword = "password",
                SmtpDomainName = "smtp.gmail.com",
                SmtpPort = 465,
                AppUserId = Id,
            };

            context.Add(mailSetting);
            context.SaveChanges();

        }

    }

    public async Task<Result<MailSetting>> GetByUserId(Guid Id, CancellationToken cancellationToken)
    {
        MailSetting? mailSetting = await context.MailSettings.SingleOrDefaultAsync(s => s.AppUserId == Id);
        if (mailSetting is null)
        {
            return Result<MailSetting>.Failure("Mail ayarları bulunamadı");
        }

        return Result<MailSetting>.Succeed(mailSetting);
    }

    public async Task<Result<string>> Update(MailSetting mailSetting, CancellationToken cancellationToken)
    {
        context.Update(mailSetting);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Mail ayarları güncellendi");
    }

    public MailSetting? GetById(Guid Id)
    {
        return context.MailSettings.SingleOrDefault(s => s.Id == Id);
    }
}
