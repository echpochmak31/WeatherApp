using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WeatherApp.DataAccess;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Services.Daemons;
using WeatherApp.Services.Security;
using WeatherApp.Services.WebClients;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<WeatherApiWebClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddSingleton<IScheduledTask, AddNewLocationsWeatherTask>();
builder.Services.AddSingleton<IScheduledTask, UpdateWeatherInfoTask>();
builder.Services.AddHostedService<ScheduledTaskService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["WEATHER_APP_SECRET_KEY"] ?? throw new
            InvalidOperationException())),
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<WeatherContext>(options =>
{
    options.UseNpgsql(config["WEATHER_APP_POSTGRESQL_CONN_STR"]);
});

var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();