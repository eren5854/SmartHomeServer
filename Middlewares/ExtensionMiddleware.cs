using Microsoft.AspNetCore.Identity;
using SmartHomeServer.Models;

namespace SmartHomeServer.Middlewares;

public static class ExtensionMiddleware
{
    public static void CreateAdmin(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any(p => p.Email == "info@marqexmarine.com"))
            {
                AppUser user = new()
                {
                    FirstName = "İhsan Eren",
                    LastName = "Delibaş",
                    UserName = "Eren",
                    Email = "info@erendelibas.com",
                    IsDeleted = false,
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                userManager.CreateAsync(user, "Password123*").Wait();
            }
        }
    }
}
