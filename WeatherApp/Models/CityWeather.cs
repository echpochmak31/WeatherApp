using Lombok.NET;

namespace WeatherApp.Models;

public class CityWeather
{
    public string City { get; set; }
    public float TemperatureСelsius { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
};