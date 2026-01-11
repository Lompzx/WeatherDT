using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WeatherServices.SharedKernel.Configurations.AutoMapper;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.AddAutoMapper(assembly);

        return services;
    }

    public static IServiceCollection AddAutoMapperServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddAutoMapper(configuration =>
        {
            //If necessary, additional configuration can be added here
        },
        assemblies);

        return services;
    }

    public static IServiceCollection AddAutoMapperServices<TProfile>(this IServiceCollection services)
    where TProfile : Profile, new()
    {
        services.AddAutoMapper(configuration => configuration.AddProfile(new TProfile()));
        return services;
    }   
}
