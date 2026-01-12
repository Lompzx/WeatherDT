using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherServices.DatabaseManagement.Abstractions;
using WeatherServices.DatabaseManagement.EntityFramework.Pipeline.MediatR;
using WeatherServices.DatabaseManagement.Internal;
using WeatherServices.DatabaseManagement.Internal.Factories;
using WeatherServices.SharedKernel.Configurations.Api;
using WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;

namespace WeatherServices.DatabaseManagement.IoC;

public static class DependencyInjectionResolver
{
    public static IServiceCollection AddEntityFrameworkCore<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        services.AddScoped<IDbFacade, DbFacade<TDbContext>>();
        services.AddScoped<ITransactionalPipelineBehavior, TransactionalPipelineBehavior<TDbContext>>();        

        services.AddDbContext<TDbContext>((serviceProvider, builder) =>
        {
            ApiProvider apiProvider = serviceProvider.GetRequiredService<ApiProvider>();

            builder.UseSqlServer(apiProvider.GetConnectionString(), options =>
            {
                options.CommandTimeout(120);
            });

            builder.EnableDetailedErrors();
        }, 
        contextLifetime: ServiceLifetime.Scoped);        

        return services;
    }

    public static IServiceCollection AddDapperFacade(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();        

        return services;
    }
}
