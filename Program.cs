using DefaultCorsPolicyNugetPackage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
builder.Services.AddAuthentication()
    .AddJwtBearer();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<AppUserRepository>();
builder.Services.AddScoped<AppUserService>();

builder.Services.AddScoped<SensorRepository>();
builder.Services.AddScoped<SensorService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SensorHub>("/sensor-hub");

app.Run();
