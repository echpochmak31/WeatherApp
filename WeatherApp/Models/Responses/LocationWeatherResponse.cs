namespace WeatherApp.Models.Responses;

public record Location(decimal lat, decimal lon, string name, string region, string country);

public record Condition(string text, string icon, int code);

public record Current(decimal temp_c, decimal feelslike_c, int last_updated_epoch, Condition condition);

public record LocationWeatherResponse
{
    public Location location { get; init; }
    public Current current { get; init; }
}