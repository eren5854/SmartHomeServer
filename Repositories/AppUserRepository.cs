using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class AppUserRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(AppUser appUser, CancellationToken cancellationToken)
    {
        await context.AddAsync(appUser);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kullanıcı kayıt işlemi başarılı");
    }

    public async Task<Result<List<AppUser>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await context.Users.ToListAsync(cancellationToken);
        return Result<List<AppUser>>.Succeed(users);
    }

    public async Task<Result<AppUser>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        AppUser? appUser = await context.Users.Include(p => p.Sensors).SingleOrDefaultAsync(s => s.Id == Id);
        if (appUser is null)
        {
            return Result<AppUser>.Failure("Kullanıcı bulunamadı");
        }
        return Result<AppUser>.Succeed(appUser);
    }

    public AppUser? GetById(Guid Id)
    {
        return context.Users.SingleOrDefault(s => s.Id == Id);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        AppUser? appUser = GetById(Id);
        if (appUser is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }
        
        appUser.IsDeleted = true;
        context.Update(appUser);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kullanıcı silme işlemi başarılı");
    }

    public async Task<Result<string>> Update(AppUser appUser, CancellationToken cancellationToken)
    {
        context.Update(appUser);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kullanıcı güncelleme işlemi başarılı");
    }
}
