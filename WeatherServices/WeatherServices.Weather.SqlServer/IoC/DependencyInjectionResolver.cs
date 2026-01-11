using Microsoft.Extensions.DependencyInjection;
using WeatherServices.DatabaseManagement.IoC;
using WeatherServices.Weather.SqlServer.EntityFramework;

namespace WeatherServices.Weather.SqlServer.IoC;

public static class DependencyInjectionResolver
{
    public static IServiceCollection AddSqlServerAdapter(this IServiceCollection services)
    {
        services.AddEntityFrameworkCore<WeatherDbContext>();       

        return services;
    }
}
