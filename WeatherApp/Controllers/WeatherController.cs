using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : Controller
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public LocationWeather GetCityWeather([FromQuery] string location)
    {
        return _weatherService.GetCurrentWeather(location);
    }

    [HttpGet("location/lookup")]
    public List<Location> CityLookup([FromQuery] string location)
    {
        return _weatherService.LocationsLookup(location);
    }
}