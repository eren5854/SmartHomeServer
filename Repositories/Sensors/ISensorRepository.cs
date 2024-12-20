using ED.Result;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Repositories.Sensors;

public interface ISensorRepository
{
    public Task<Result<string>> Create(Sensor sensor, CancellationToken cancellationToken);
    public Task<string> CreateSerialNo(Sensor sensor);
    public Task<Result<List<Sensor>>> GetAllSensorByUserId(Guid Id, CancellationToken cancellation);
    public Task<Result<GetAllSensorDataDto>> GetBySecretKey(string SecretKey, CancellationToken cancellationToken);
    public Task<Result<Sensor>> GetById(Guid Id, CancellationToken cancellationToken);
    public Sensor? GetById(Guid Id);
    public Sensor? GetBySecretKey(string SecretKey);
    public Task<Result<string>> DeleteById(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> Update(Sensor sensor, CancellationToken cancellationToken);

    //Admin
    public Task<Result<List<Sensor>>> GetAll(CancellationToken cancellationToken);
    public Task<Result<List<GetAllSensorDataDto>>> GetAllSensorData(CancellationToken cancellationToken);
}
