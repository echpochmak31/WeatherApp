using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Models.Dto;
using WeatherApp.Models.Requests;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

[Authorize]
[ApiController]
[Route("weather")]
public class WeatherController : Controller
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IGroupService _groupService;

    public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IGroupService groupService)
    {
        _logger = logger;
        _weatherService = weatherService;
        _groupService = groupService;
    }

    [HttpGet("current")]
    public LocationWeather GetCurrentWeatherAsync([FromQuery] string location)
    {
        return _weatherService.GetCurrentWeatherAsync(location).Result;
    }

    [HttpGet("location/lookup")]
    public List<LocationDto> LocationsLookup([FromQuery] string location)
    {
        return _weatherService.LocationsLookup(location);
    }

    [HttpPost("location/group")]
    public IActionResult AddLocationGroup([FromBody] AddLocationGroupRequest request)
    {
        _groupService.AddLocationGroup(request.UserEmail, request.GroupName, request.Locations);
        return Ok();
    }
    
    [HttpGet("location/group")]
    public List<LocationGroupDto> GetUserLocationGroups([FromQuery] string userEmail)
    {
        return _groupService.GetUserLocationGroups(userEmail);
        
    }
    
    [HttpGet("location/group/info")]
    public List<LocationWeatherDto> GetUserLocationWeatherInfo([FromQuery] string userEmail)
    {
        return _groupService.GetUserLocationWeahterInfo(userEmail);
    }
    
}