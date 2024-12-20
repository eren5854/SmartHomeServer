using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Scenarios;

public sealed class ScenarioRepository(
    ApplicationDbContext context) : IScenarioRepository
{
    public async Task<Result<string>> Create(Scenario scenario, CancellationToken cancellationToken)
    {
        await context.AddAsync(scenario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Senaryo kaydı başarılı");
    }

    public async Task<Result<List<Scenario>>> GetAllScenarioByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var scenarios = await context.Scenarios
     .Where(p => p.AppUserId == Id)
     .Include(i => i.Trigger)
         .ThenInclude(i => i!.Sensor) // Trigger'daki Sensor bilgisini dahil et
     .Include(i => i.Trigger)
         .ThenInclude(i => i!.Action) // Trigger'daki Action bilgisini dahil et
         .ThenInclude(a => a.Sensor) // Action'daki Sensor bilgisini dahil et
     .ToListAsync(cancellationToken);
        return Result<List<Scenario>>.Succeed(scenarios);
    }

    public async Task<Result<Scenario>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var scenario = await context.Scenarios
            .Where(p => p.Id == Id)
            .Include(i => i.Trigger)
                .ThenInclude(i => i!.Sensor) // Trigger'daki Sensor bilgisini dahil et
            .Include(i => i.Trigger)
                .ThenInclude(i => i!.Action) // Trigger'daki Action bilgisini dahil et
                .ThenInclude(a => a.Sensor) // Action'daki Sensor bilgisini dahil et
            .FirstOrDefaultAsync();

        return Result<Scenario>.Succeed(scenario!);
    }

    public Scenario? GetById(Guid Id)
    {
        return context.Scenarios.SingleOrDefault(s => s.Id == Id);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var scenario = await context.Scenarios
            .Include(s => s.Trigger)
            .ThenInclude(t => t!.Action)
            .FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);

        if (scenario is null)
        {
            return Result<string>.Failure("Senaryo bulunamadı");
        }

        scenario.IsDeleted = true;
        if (scenario.Trigger != null)
        {
            scenario.Trigger.IsDeleted = true;
            if (scenario.Trigger.Action != null)
            {
                scenario.Trigger.Action.IsDeleted = true;
            }
        }

        context.Update(scenario);
        await context.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Senaryo silme başarılı");
    }

    public async Task<Result<string>> Update(Scenario scenario, CancellationToken cancellationToken)
    {
        context.Update(scenario);
        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Senaryo güncelleme başarılı");
    }
}
