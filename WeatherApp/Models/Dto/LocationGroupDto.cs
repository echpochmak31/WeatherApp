using Lombok.NET;

namespace WeatherApp.Models.Dto;

public record LocationGroupDto(int Id, string UserEmail, string GroupName, List<Coordinates> Locations)
{
    public LocationGroupDto() : this(0, string.Empty, string.Empty, new List<Coordinates>())
    {
    }
}