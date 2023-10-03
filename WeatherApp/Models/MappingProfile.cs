﻿using AutoMapper;
using WeatherApp.Models.Responses;

namespace WeatherApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CityLookupResponse, City>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.lon));

        CreateMap<CityWeatherResponse, CityWeather>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.location.name))
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.current.last_updated_epoch)))
            .ForMember(dest => dest.TemperatureСelsius, opt => opt.MapFrom(src => src.current.temp_c));

    }
}