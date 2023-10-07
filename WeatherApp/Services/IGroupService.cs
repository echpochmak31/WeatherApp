using WeatherApp.DataAccess;
using WeatherApp.Models;
using WeatherApp.Models.Dto;

namespace WeatherApp.Services;

public interface IGroupService
{
    public void AddLocationGroup(string userEmail, string groupName, IEnumerable<Coordinates> locations);
    public List<LocationGroupDto> GetUserLocationGroups(string userEmail);
}