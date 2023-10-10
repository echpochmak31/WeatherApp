using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherService
{
    public Task<LocationWeather> GetCurrentWeatherAsync(string q);
    public List<Location> LocationsLookup(string q);
}