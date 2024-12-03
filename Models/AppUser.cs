using Microsoft.AspNetCore.Identity;
using SmartHomeServer.Entities;
using SmartHomeServer.Enums;

namespace SmartHomeServer.Models;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);

    public List<Sensor>? Sensors { get; set; }
    public List<Room>? Rooms { get; set; }

    public UserRoleEnum Role { get; set; } = UserRoleEnum.User;

    public string? RefreshToken {  get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public string SecretToken { get; set; } = default!;

    public int? ForgotPasswordCode { get; set; }
    public DateTime? ForgotPasswordCodeSendDate { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
