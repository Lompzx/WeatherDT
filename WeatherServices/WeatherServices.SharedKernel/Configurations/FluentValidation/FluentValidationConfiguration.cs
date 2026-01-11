using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WeatherServices.SharedKernel.Configurations.FluentValidation;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidationValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddFluentValidationValidators(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
