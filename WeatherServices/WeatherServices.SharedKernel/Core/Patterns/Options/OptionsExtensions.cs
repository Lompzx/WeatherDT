using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WeatherServices.SharedKernel.Core.Patterns.Options;

public static class OptionsExtensions
{
    public static IServiceCollection RegisterOptionsOf<T>(
        this IServiceCollection services,
        string sectionName,
        bool validateOnStart = true) where T : class
    {
        OptionsBuilder<T> optionsBuilder = services.AddOptions<T>().BindConfiguration(sectionName);

        if (validateOnStart)
            optionsBuilder.ValidateDataAnnotations().ValidateOnStart();

        return services;
    }   
}
