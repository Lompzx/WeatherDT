using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherServices.WeatherAPI.Factories;
using WeatherServices.WeatherAPI.Weather;

namespace WeatherServices.WeatherAPI;

public static class WeatherConfiguration 
{
    public static IServiceCollection AddWeatherService(
        this IServiceCollection services,
        IConfiguration configuration, 
        string sectionName = WeatherOptions.SectionName)
    {       

        services.AddWeatherAPI(configuration, sectionName);

        services.AddScoped<IWeatherAPIFactory, WeatherAPIFactory>();

        return services;
    }
}
