using AutoMapper;
using Azure.Core;
using SmartHomeServer.DTOs.AppUserDto;
using SmartHomeServer.DTOs.RemoteControlDto;
using SmartHomeServer.DTOs.RemoteControlKeyDto;
using SmartHomeServer.DTOs.RoomDto;
using SmartHomeServer.DTOs.ScenarioDto;
using SmartHomeServer.DTOs.SensorDto;
using SmartHomeServer.DTOs.TemplateSettingDto;
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

        CreateMap<UpdateTemplateSettingDto, TemplateSetting>();

        //CreateMap<CreateRemoteControlDto, RemoteControl>()
        //    .ForMember(dest => dest.RemoteControlKeys, opt => opt.MapFrom(src =>
        //        src.CreateRemoteControlKeys.Select(key => new RemoteControlKey
        //        {
        //            KeyName = key.KeyName,
        //            KeyCode = key.KeyCode
        //        }).ToList()));

        CreateMap<CreateRemoteControlDto, RemoteControl>();

        //CreateMap<UpdateRemoteControlDto, RemoteControl>()
        //    .ForMember(dest => dest.RemoteControlKeys, opt => opt.MapFrom(src =>
        //        src.UpdateRemoteControlKeys.Select(key => new RemoteControlKey
        //        {
        //            KeyName = key.KeyName,
        //            KeyCode = key.KeyCode
        //        }).ToList()));

        CreateMap<UpdateRemoteControlDto, RemoteControl>();

        CreateMap<UpdateRemoteControlKeyDto, RemoteControlKey>()
          .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}
