using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherService
{
    public LocationWeather GetCurrentWeather(string q);
    public List<Location> LocationsLookup(string q);
}