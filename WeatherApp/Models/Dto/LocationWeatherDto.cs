namespace WeatherApp.Models.Dto;

public record Location(decimal Latitude, decimal Longitude, string Name, string Region, string Country)
{
    public Location() : this(0, 0, string.Empty, string.Empty, string.Empty)
    {
    }
}

public record Condition(string Text, string Icon, int Code)
{
    public Condition() : this(string.Empty, string.Empty, 0)
    {
    }
}

public record Current(decimal TempCelsius, decimal FeelsLikeCelsius, int LastUpdated, Condition Condition)
{
    public Current() : this(0, 0, 0, null!)
    {
    }
}

public record LocationWeatherDto
{
    public Location Location { get; init; }
    public Current Current { get; init; }
}