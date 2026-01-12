using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherServices.SharedKernel.Configurations.MediatR.Pipelines;

namespace WeatherServices.SharedKernel.Configurations.MediatR;

public static class MediatRConfiguration
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(assemblies);
            options.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            options.NotificationPublisher = new TaskWhenAllPublisher();
        });               

        return services;
    }

    public static IServiceCollection WithValidationPipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }

    public static IServiceCollection WithLoggingPipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        return services;
    }

    public static IServiceCollection WithDbTransactionPipeline(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbTransactionPipelineBehavior<,>));
        return services;
    }
}
