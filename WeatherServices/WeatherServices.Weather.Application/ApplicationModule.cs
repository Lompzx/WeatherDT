using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherServices.SharedKernel;
using WeatherServices.SharedKernel.Configurations.AutoMapper;
using WeatherServices.SharedKernel.Configurations.FluentValidation;
using WeatherServices.SharedKernel.Configurations.MediatR;
using WeatherServices.Weather.Domain;
using WeatherServices.Weather.SqlServer;
using WeatherServices.Weather.SqlServer.IoC;
using WeatherServices.WeatherAPI;

namespace WeatherServices.Weather.Application;

public static class ApplicationModule
{
    private static readonly Assembly[] SolutionAssemblies =
    [
        ApplicationAssemblyReference.Assembly,
        SharedKernelAssemblyReference.Assembly,
        DomainAssemblyReference.Assembly,
        SqlServerAdapterAssemblyReference.Assembly,
        WeatherAPIAssemblyReference.Assembly
    ];

    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExternalDependencies(configuration);

        services.AddAdapters(configuration);                

        return services;
    }

    private static IServiceCollection AddExternalDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatRServices(SolutionAssemblies)
                .WithValidationPipeline()                
                .WithDbTransactionPipeline()                
                .WithLoggingPipeline();

        services.AddFluentValidationValidators(SolutionAssemblies);
        services.AddAutoMapperServices(SolutionAssemblies);

        services.AddWeatherService(configuration);

        return services;
    }

    private static IServiceCollection AddAdapters(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServerAdapter();

        return services;
    }
}
