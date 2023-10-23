using AutoMapper;
using WeatherApp.Models.Dto;
using WeatherApp.Models.Responses;

namespace WeatherApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LocationLookupResponse, LocationDto>()
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.region))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.lon));


        CreateMap<LocationWeather, LocationWeatherDto>()
            .ForPath(dest => dest.Location.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.Location.Region, opt => opt.MapFrom(src => src.Region))
            .ForPath(dest => dest.Location.Country, opt => opt.MapFrom(src => src.Country))
            .ForPath(dest => dest.Location.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForPath(dest => dest.Location.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForPath(dest => dest.Current.LastUpdated,
                opt => opt.MapFrom(src => src.LastUpdated.ToUnixTimeSeconds()))
            .ForPath(dest => dest.Current.TempCelsius, opt => opt.MapFrom(src => src.TemperatureСelsius))
            .ForPath(dest => dest.Current.FeelsLikeCelsius, opt => opt.MapFrom(src => src.FeelsLikeСelsius))
            .ForPath(dest => dest.Current.Condition.Text, opt => opt.MapFrom(src => src.ConditionText))
            .ForPath(dest => dest.Current.Condition.Icon, opt => opt.MapFrom(src => src.ConditionImageUrl))
            .ForPath(dest => dest.Current.Condition.Code, opt => opt.MapFrom(src => src.ConditionCode));

        CreateMap<LocationGroup, LocationGroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
            .ForMember(dest => dest.Locations,
                opt => opt.MapFrom(src => src.Items.Select(x => new Coordinates(x.Latitude, x.Longitude))));
    }
}