using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess;

namespace WeatherApp.Services.Daemons;

public class UpdateWeatherInfoTask : IScheduledTask
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly int _periodInMinutes;

    public UpdateWeatherInfoTask(IServiceScopeFactory scopeFactory, IConfiguration configuration)

    {
        _scopeFactory = scopeFactory;
        _periodInMinutes = Convert.ToInt32(configuration["Scheduled:UpdateWeatherInfoPeriodInMinutes"]);
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();

                var thirtyMinutesAgo = DateTime.Now.AddMinutes(-30);

                var outdatedLocationWeathers = dbContext.LocationWeathers
                    .Where(weather => weather.LastUpdated <= thirtyMinutesAgo)
                    .ToList();

                var updateTasks = outdatedLocationWeathers.Select(async weather =>
                {
                    var newWeather =
                        await weatherService.GetCurrentWeatherAsync($"{weather.Latitude},{weather.Longitude}");

                    weather.Name = newWeather.Name;
                    weather.Region = newWeather.Region;
                    weather.Country = newWeather.Country;
                    weather.LastUpdated = newWeather.LastUpdated;
                    weather.TemperatureСelsius = newWeather.TemperatureСelsius;
                    weather.FeelsLikeСelsius = newWeather.FeelsLikeСelsius;
                    weather.ConditionText = newWeather.ConditionText;
                    weather.ConditionImageUrl = newWeather.ConditionImageUrl;
                    weather.ConditionCode = newWeather.ConditionCode;
                });

                await Task.WhenAll(updateTasks);

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(_periodInMinutes), cancellationToken);
        }
    }
}