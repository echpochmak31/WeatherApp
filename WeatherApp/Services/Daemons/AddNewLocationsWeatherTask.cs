using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess;

namespace WeatherApp.Services.Daemons;

public class AddNewLocationsWeatherTask : IScheduledTask
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly int _periodInSeconds;

    public AddNewLocationsWeatherTask(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _periodInSeconds = Convert.ToInt32(configuration["Scheduled:AddNewLocationsWeatherPeriodInSeconds"]);
    }
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();

                var itemsWithoutWeather = await dbContext.LocationGroupItems
                    .Where(item => !dbContext.LocationWeathers
                        .Any(weather => weather.LocationGroupItemLatitude == item.Latitude &&
                                        weather.LocationGroupItemLongitude == item.Longitude))
                    .ToListAsync(cancellationToken: cancellationToken);
                    
                var tasks = itemsWithoutWeather.Select(async item =>
                {
                    var weather = await weatherService.GetCurrentWeatherAsync($"{item.Latitude},{item.Longitude}");
                    weather.LocationGroupItemLatitude = item.Latitude;
                    weather.LocationGroupItemLongitude = item.Longitude;
                    weather.LocationGroupItem = item;
                    await dbContext.AddAsync(weather, cancellationToken);
                });

                await Task.WhenAll(tasks);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(_periodInSeconds), cancellationToken);
        }
    }
}