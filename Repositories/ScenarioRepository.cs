using ED.Result;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.DTOs.ScenarioDto;
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
        var scenarios = await context.Scenarios.Where(p => p.AppUserId == Id).Include(i => i.Trigger).Include(i => i.Trigger!.Action).ToListAsync(cancellationToken);
        return Result<List<Scenario>>.Succeed(scenarios);
    }

    public Scenario? GetById(Guid Id)
    {
        return context.Scenarios.SingleOrDefault(s => s.Id == Id);
    }

    public async Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        Scenario? scenario = GetById(Id);
        if (scenario is null)
        {
            return Result<string>.Failure("Senaryo bulunamadı");
        }

        Trigger? trigger = context.Triggers.Where(p => p.Id == scenario.TriggerId).FirstOrDefault();
        if (trigger is null)
        {
            return Result<string>.Failure("Tetikleyici bulunamadı");
        }

        Models.Action? action = context.Actions.SingleOrDefault(p => p.Id == trigger.ActionId);
        if (action is null)
        {
            return Result<string>.Failure("Aksiyon bulunamadı");
        }

        scenario.IsDeleted = true;
        scenario.Trigger!.IsDeleted = true;
        scenario.Trigger.Action!.IsDeleted = true;

        context.Update(scenario);
        context.Update(trigger);
        context.Update(action);

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
