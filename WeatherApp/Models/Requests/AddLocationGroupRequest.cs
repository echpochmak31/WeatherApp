namespace WeatherApp.Models.Requests;

public record AddLocationGroupRequest(string UserEmail, string GroupName, List<Coordinates> Locations);