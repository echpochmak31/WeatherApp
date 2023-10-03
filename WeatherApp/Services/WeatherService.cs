using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Models.Responses;
using WeatherApp.Services.WebClients;
using AutoMapper;

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

    public CityWeather GetCityCurrentWeather(string city)
    {
        ArgumentNullException.ThrowIfNull(city);
        var jsonResponse = _webClient.GetCityCurrentWeather(city);
        var cityWeatherResponse = JsonSerializer.Deserialize<CityWeatherResponse>(jsonResponse);
        return _mapper.Map<CityWeather>(cityWeatherResponse);
    }

    public List<City> CityLookup(string q)
    {
        var jsonResponse = _webClient.CityLookup(q);
        var cityLookupResponses = JsonSerializer.Deserialize<List<CityLookupResponse>>(jsonResponse);
        return _mapper.Map<List<City>>(cityLookupResponses);
    }
}