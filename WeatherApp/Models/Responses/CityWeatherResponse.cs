namespace WeatherApp.Models.Responses;

public record Location(float lat, float lon, string name, string country);

public record Current(float temp_c, int last_updated_epoch);

public record LocationWeatherResponse
{
    public Location location { get; init; }
    public Current current { get; init; }   
}