using AutoMapper;
using WeatherApp.Models.Dto;
using WeatherApp.Models.Responses;

namespace WeatherApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LocationLookupResponse, Location>()
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.region))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.lon));


        CreateMap<LocationWeatherResponse, LocationWeather>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.location.name))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.location.region))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.location.country))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.location.lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.location.lon))
            .ForMember(dest => dest.LastUpdated,
                opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.current.last_updated_epoch)))
            .ForMember(dest => dest.TemperatureСelsius, opt => opt.MapFrom(src => src.current.temp_c))
            .ForMember(dest => dest.FeelsLikeСelsius, opt => opt.MapFrom(src => src.current.feelslike_c))
            .ForMember(dest => dest.ConditionText, opt => opt.MapFrom(src => src.current.condition.text))
            .ForMember(dest => dest.ConditionImageUrl, opt => opt.MapFrom(src => src.current.condition.icon))
            .ForMember(dest => dest.ConditionCode, opt => opt.MapFrom(src => src.current.condition.code));

        CreateMap<LocationGroup, LocationGroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
            .ForMember(dest => dest.Locations,
                opt => opt.MapFrom(src => src.Items.Select(x => new Coordinates(x.Latitude, x.Longitude))));
    }
}