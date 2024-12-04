using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories;

public sealed class ScenarioRepository(
    ApplicationDbContext context)
{
    public async Task<Result<string>> Create(Scenario scenario, CancellationToken cancellationToken)
    {
        await context.AddAsync(scenario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Senaryo kaydı başarılı");
    }

    public async Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var scenarios = await context.Scenarios.Where(p => p.AppUserId == Id).ToListAsync(cancellationToken);
        return Result<List<Scenario>>.Succeed(scenarios);
    }
}
