namespace SmartHomeServer.DTOs.TemplateSettingDto;

public sealed record UpdateTemplateSettingDto(
    Guid Id,
    string ContainerLayout,
    string HeaderBg,
    string HeaderPosition,
    string Layout,
    string NavHeaderBg,
    string Primary,
    string SidebarBg,
    string SidebarPosition,
    string SidebarStyle,
    string Typography,
    string Version);