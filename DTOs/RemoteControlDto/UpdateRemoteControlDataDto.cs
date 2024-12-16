namespace SmartHomeServer.DTOs.RemoteControlDto;

public sealed record UpdateRemoteControlDataDto(
    string SecretKey,
    bool? OnOff,
    bool? NextChannel,
    bool? PrevChannel,
    bool? VolumeUp,
    bool? VolumeDown,
    bool? ChannelMenu,
    bool? Source);
