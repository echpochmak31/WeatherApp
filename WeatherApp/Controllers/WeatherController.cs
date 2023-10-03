using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

[ApiController]
[Route("weather/city")]
public class WeatherController : Controller
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("current/{cityName}")]
    public CityWeather GetCityWeather([FromRoute] string cityName)
    {
        return _weatherService.GetCityCurrentWeather(cityName);
    }

    [HttpGet("lookup/{cityName}")]
    public List<City> CityLookup([FromRoute] string cityName)
    {
        return _weatherService.CityLookup(cityName);
    }
}