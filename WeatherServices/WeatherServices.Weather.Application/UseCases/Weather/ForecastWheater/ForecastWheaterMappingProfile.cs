using WeatherServices.SharedKernel.Configurations.AutoMapper;
using WeatherServices.WeatherAPI.WeatherAPI.DTOs;

namespace WeatherServices.Weather.Application.UseCases.ForecastWheater;

internal sealed class ForecastWheaterMappingProfile : ApplicationMappingProfile
{
    protected override void RegisterMappings()
    {
        CreateMap<WeatherForecast, ForecastWheaterResponse>();
        CreateMap<DailyForecast, DailyForecastResponse>();
    }
}
