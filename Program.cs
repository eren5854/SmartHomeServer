using DefaultCorsPolicyNugetPackage;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartHomeServer.BackgroundServices;
using SmartHomeServer.Context;
using SmartHomeServer.Hubs;
using SmartHomeServer.Mapper;
using SmartHomeServer.Middlewares;
using SmartHomeServer.Models;
using SmartHomeServer.Options;
using SmartHomeServer.Repositories.LightTimeLogs;
using SmartHomeServer.Repositories.MailSettings;
using SmartHomeServer.Repositories.Notifications;
using SmartHomeServer.Repositories.RemoteControlKeys;
using SmartHomeServer.Repositories.RemoteControls;
using SmartHomeServer.Repositories.Rooms;
using SmartHomeServer.Repositories.Scenarios;
using SmartHomeServer.Repositories.Sensors;
using SmartHomeServer.Repositories.TemplateSettings;
using SmartHomeServer.Repositories.Users;
using SmartHomeServer.Services;
using SmartHomeServer.Services.LightTimeLogs;
using SmartHomeServer.Services.MailSettings;
using SmartHomeServer.Services.Notifications;
using SmartHomeServer.Services.RemoteControlKeys;
using SmartHomeServer.Services.RemoteControls;
using SmartHomeServer.Services.Rooms;
using SmartHomeServer.Services.Scenarios;
using SmartHomeServer.Services.Sensors;
using SmartHomeServer.Services.TemplateSettings;
using SmartHomeServer.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddHangfireServer();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
    options.Lockout.MaxFailedAccessAttempts = 50;
    options.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureOptions<JwtTokenSetupConfiguration>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/sensor-hub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorizationBuilder();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();

builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<ISensorService, SensorService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();
builder.Services.AddScoped<IScenarioService, ScenarioService>();

builder.Services.AddScoped<ILightTimeLogRepository, LightTimeLogRepository>();
builder.Services.AddScoped<ILightTimeLogService, LightTimeLogService>();

builder.Services.AddScoped<ITemplateSettingRepository, TemplateSettingRepository>();
builder.Services.AddScoped<ITemplateSettingService, TemplateSettingService>();

builder.Services.AddScoped<IRemoteControlRepository, RemoteControlRepository>();
builder.Services.AddScoped<IRemoteControlService, RemoteControlService>();

builder.Services.AddScoped<IRemoteControlKeyRepository, RemoteControlKeyRepository>();
builder.Services.AddScoped<IRemoteControlKeyService, RemoteControlKeyService>();

builder.Services.AddScoped<IMailSettingRepository, MailSettingRepository>();
builder.Services.AddScoped<IMailSettingService, MailSettingService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

builder.Services.AddScoped<ISensorRepository, SensorRepository>();


builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

ExtensionMiddleware.CreateAdmin(app);

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<TimeBackgroundService>(x => x.ValueTrigger(), "*/10 * * * * *");
RecurringJob.AddOrUpdate<TimeBackgroundService>(x => x.TimeTrigger(), Cron.Minutely());
//RecurringJob.AddOrUpdate<AutoGetBackgroundService>(x => x.GetAllSensor(), Cron.Minutely());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<SensorHub>("/sensor-hub");

app.Run();
