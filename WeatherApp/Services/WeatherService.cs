using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Models.Responses;
using WeatherApp.Services.WebClients;
using AutoMapper;
using Location = WeatherApp.Models.Location;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private readonly WeatherApiWebClient _webClient;
    private readonly IMapper _mapper;

    
    public WeatherService(WeatherApiWebClient weatherApiWebClient, IMapper mapper)
    {
        _webClient = weatherApiWebClient;
        _mapper = mapper;
    }

    public LocationWeather GetCurrentWeather(string q)
    {
        ArgumentNullException.ThrowIfNull(q);
        var jsonResponse = _webClient.GetCurrentWeather(q);
        var cityWeatherResponse = JsonSerializer.Deserialize<LocationWeatherResponse>(jsonResponse);
        return _mapper.Map<LocationWeather>(cityWeatherResponse);
    }

    public List<Location> LocationsLookup(string q)
    {
        ArgumentNullException.ThrowIfNull(q);
        var jsonResponse = _webClient.LocationLookup(q);
        var cityLookupResponses = JsonSerializer.Deserialize<List<LocationLookupResponse>>(jsonResponse);
        return _mapper.Map<List<Location>>(cityLookupResponses);
    }
}