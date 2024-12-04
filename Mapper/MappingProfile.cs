using AutoMapper;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.Models;

namespace SmartHomeServer.Mapper;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAppUserDto, AppUser>();
        CreateMap<UpdateAppUserDto, AppUser>();

        CreateMap<CreateSensorDto, Sensor>();
        CreateMap<UpdateSensorDto, Sensor>();
        CreateMap<UpdateSensorDataDto, Sensor>();

        CreateMap<CreateRoomDto, Room>();
        CreateMap<UpdateRoomDto, Room>();

        CreateMap<CreateScenarioDto, Scenario>();
    }
}
