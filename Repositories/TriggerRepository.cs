using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class TriggerRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(Trigger trigger, CancellationToken cancellationToken)
    {
        await context.AddAsync(trigger, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Tetikleme kaydı başarılı");
    }

    public async Task<Result<List<Trigger>>> GetAllTriggerByScenarioId(Guid Id, CancellationToken cancellationToken)
    {
        var triggers = await context.Triggers.Where(p => p.ScenarioId == Id).ToListAsync(cancellationToken);
        return Result<List<Trigger>>.Succeed(triggers);
    }
}
