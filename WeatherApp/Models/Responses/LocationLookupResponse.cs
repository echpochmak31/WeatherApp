namespace WeatherApp.Models.Responses;

public record LocationLookupResponse(int id, string name, string region, string country, float lat, float lon);
