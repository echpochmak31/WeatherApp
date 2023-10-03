namespace WeatherApp.Models.Responses;

public record CityLookupResponse(int id, string name, string region, string country, float lat, float lon);
