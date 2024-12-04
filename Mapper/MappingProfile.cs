using AutoMapper;
using Azure.Core;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.DTOs.TriggerDto;
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

        CreateMap<CreateScenarioDto, Scenario>()
            .ForMember(member => member.Trigger, opt => opt.MapFrom(src => new Trigger
            {
                SensorId = src.TriggerSensorId,
                TriggerType = src.TriggerType,
                TriggerValue = src.TriggerValue,
                TriggerTime = src.TriggerTime,
                Action = new()
                {
                    SensorId = src.ActionSensorId,
                    ActionType = src.ActionType,
                    Value = src.ActionValue
                }
            }));
        CreateMap<UpdateScenarioDto, Scenario>()
            .ForMember(member => member.Trigger, opt => opt.MapFrom(src => new Trigger
            {
                SensorId = src.TriggerSensorId,
                TriggerType = src.TriggerType,
                TriggerValue = src.TriggerValue,
                TriggerTime = src.TriggerTime,
                Action = new()
                {
                    SensorId = src.ActionSensorId,
                    ActionType = src.ActionType,
                    Value = src.ActionValue
                }
            }));

        CreateMap<CreateTriggerDto, Trigger>();
    }
}
