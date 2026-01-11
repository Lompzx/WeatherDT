using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;
using WeatherServices.SharedKernel.Core.Patterns.Options;

namespace WeatherServices.WeatherAPI.Weather;

internal static class DependencyInjectionResolver
{
    internal static IServiceCollection AddWeatherAPI(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddDefaultOptions();
        services.AddWeatherAPIHttpHandler(configuration, sectionName);
        return services;
    }

    private  static IServiceCollection AddWeatherAPIHttpHandler(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        WeatherOptions? options = configuration.GetSection(sectionName).Get<WeatherOptions>();

        ArgumentNullException.ThrowIfNull(options);

        services.AddHttpClient(WeatherOptions.SectionName, (serviceProvider, client) => 
        {
            client.BaseAddress = new Uri("https://api.weatherapi.com/");
        }); 

        return services;
    }

    private static IServiceCollection AddDefaultOptions(this IServiceCollection services)
    {
        services.RegisterOptionsOf<WeatherOptions>(WeatherOptions.SectionName);
        return services;
    }
}
