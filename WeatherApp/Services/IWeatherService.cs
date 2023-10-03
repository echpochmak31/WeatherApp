using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherService
{
    public CityWeather GetCityCurrentWeather(string city);
    public List<City> CityLookup(string q);
}