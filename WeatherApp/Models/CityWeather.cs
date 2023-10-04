using Lombok.NET;

namespace WeatherApp.Models;

public class LocationWeather
{
    public string Location { get; set; }
    public float TemperatureСelsius { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
};