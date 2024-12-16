namespace SmartHomeServer.DTOs.RemoteControlDto;

public sealed record CreateRemoteControlDto(
    string Name,
    string Description,
    bool? OnOff,
    bool? NextChannel,
    bool? PrevChannel,
    bool? VolumeUp,
    bool? VolumeDown,
    bool? ChannelMenu,
    bool? Source);
