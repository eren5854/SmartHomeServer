using ED.Result;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Services.Sensors;

public interface ISensorService
{
    public Task<Result<string>> Create(CreateSensorDto request, CancellationToken cancellationToken);
    public Task<Result<List<Sensor>>> GetAllSensorByUserId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(UpdateSensorDto request, CancellationToken cancellationToken);
    public Task<Result<string>> UpdateSecretKeyById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<Sensor>> GetById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<GetAllSensorDataDto>> GetBySecretKey(string SecretKey, CancellationToken cancellationToken);
    public Task<Result<string>> UpdateSensorData(UpdateSensorDataDto request, CancellationToken cancellationToken);

    public Task<Result<List<Sensor>>> GetAll(CancellationToken cancellationToken);
    public Task<Result<List<GetAllSensorDataDto>>> GetAllSensorData(CancellationToken cancellationToken);
}
