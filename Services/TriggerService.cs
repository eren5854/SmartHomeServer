using AutoMapper;
using ED.Result;
using SmartHomeServer.Repositories;

namespace SmartHomeServer.Services;

public sealed class TriggerService(
    TriggerRepository triggerRepository,
    IMapper mapper)
{
    public async Task<Result<string>> Create()
}
