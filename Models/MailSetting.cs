using SmartHomeServer.Entities;
using System.Text.Json.Serialization;

namespace SmartHomeServer.Models;

public sealed class MailSetting : Entity
{
    public string Email { get; set; } = default!;
    public string AppPassword { get; set; } = default!;
    public string SmtpDomainName { get; set; } = default!;
    public int SmtpPort { get; set; } = default!;
    public DateTime? LastSendTime { get; set; }

    [JsonIgnore]
    public Guid? AppUserId { get; set; }
    [JsonIgnore]
    public AppUser? AppUser { get; set; }
}
