using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess;
using WeatherApp.Models;
using WeatherApp.Models.Dto;
using WeatherApp.Models.Responses;
using WeatherApp.Models.Security;

namespace WeatherApp.Services;

public class GroupService : IGroupService
{
    private readonly ILogger<GroupService> _logger;
    private readonly WeatherContext _dbContext;
    private readonly IMapper _mapper;

    public GroupService(WeatherContext dbContext, ILogger<GroupService> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public void AddLocationGroup(string userEmail, string groupName, IEnumerable<Coordinates> locations)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
        ThrowIfUserDoesNotExist(user);

        var group = new LocationGroup()
        {
            UserEmail = userEmail,
            GroupName = groupName,
            UserId = user.Id,
            Items = locations.Select(x =>
            {
                var item = new LocationGroupItem();
                item.Latitude = x.Latitude;
                item.Longitude = x.Longitude;
                return item;
            }).ToList()
        };

        _dbContext.LocationGroups.Add(group);
        _dbContext.SaveChanges();
    }

    public List<LocationGroupDto> GetUserLocationGroups(string userEmail)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
        ThrowIfUserDoesNotExist(user);

        var src = _dbContext.LocationGroups
            .Where(x => x.UserId == user.Id)
            .Include(locationGroup => locationGroup.Items).ToList();
        return _mapper.Map<List<LocationGroupDto>>(src);
    }

    public List<LocationWeatherDto> GetUserLocationWeahterInfo(string userEmail)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
        ThrowIfUserDoesNotExist(user);
        
        var src = _dbContext.LocationGroups
            .Where(x => x.UserId == user.Id)
            .Include(locationGroup => locationGroup.Items)
            .SelectMany(x => x.Items);
        
        var weatherInfo = _dbContext.LocationWeathers
            .Where(weather => src.Any(item => item.Latitude == weather.Latitude && item.Longitude == weather.Longitude))
            .ToList();
        
        return _mapper.Map<List<LocationWeatherDto>>(weatherInfo);
    }

    private void ThrowIfUserDoesNotExist(User? user)
    {
        if (user is null)
        {
            throw new Exception();
        }
    }
}