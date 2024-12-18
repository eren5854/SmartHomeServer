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
using SmartHomeServer.Repositories;
using SmartHomeServer.Services;

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

builder.Services.AddScoped<AppUserRepository>();
builder.Services.AddScoped<AppUserService>();

builder.Services.AddScoped<SensorRepository>();
builder.Services.AddScoped<SensorService>();

builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<RoomService>();

builder.Services.AddScoped<ScenarioRepository>();
builder.Services.AddScoped<ScenarioService>();

builder.Services.AddScoped<LightTimeLogRepository>();
builder.Services.AddScoped<LightTimeLogService>();

builder.Services.AddScoped<TemplateSettingRepository>();
builder.Services.AddScoped<TemplateSettingService>();

builder.Services.AddScoped<RemoteControlRepository>();
builder.Services.AddScoped<RemoteControlService>();

builder.Services.AddScoped<RemoteControlKeyRepository>();
builder.Services.AddScoped<RemoteControlKeyService>();

builder.Services.AddScoped<MailSettingRepository>();
builder.Services.AddScoped<MailSettingService>();

builder.Services.AddScoped<NotificationRepository>();
builder.Services.AddScoped<NotificationService>();

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
