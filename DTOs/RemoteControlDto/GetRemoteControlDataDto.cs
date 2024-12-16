namespace SmartHomeServer.DTOs.RemoteControlDto;

public sealed record GetRemoteControlDataDto(
    string SerialNo,
    string SecretKey,
    bool? OnOff,
    bool? NextChannel,
    bool? PrevChannel,
    bool? VolumeUp,
    bool? VolumeDown,
    bool? ChannelMenu,
    bool? Source);
