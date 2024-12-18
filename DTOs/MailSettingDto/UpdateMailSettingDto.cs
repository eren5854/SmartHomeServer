namespace SmartHomeServer.DTOs.MailSettingDto;

public sealed record UpdateMailSettingDto(
    Guid Id,
    string Email,
    string AppPassword,
    string SmtpDomainName,
    int SmtpPort);
