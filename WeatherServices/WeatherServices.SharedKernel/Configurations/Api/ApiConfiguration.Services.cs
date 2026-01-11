using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Text.Json.Serialization;
using WeatherServices.SharedKernel.Configurations.Middlewares.FactoryBased;
using WeatherServices.SharedKernel.Configurations.Swagger;
using WeatherServices.SharedKernel.Core.Patterns.Options;

namespace WeatherServices.SharedKernel.Configurations.Api;

public static partial class ApiConfiguration
{

    private const int ApiDefaultMajorVersion = 1;
    private const string ApiVersionHeader = "x-api-version";
    private const string ApiVersionGroupNameFormat = "'v'V";

    public static IServiceCollection AddApiDefaultServices(this IServiceCollection services, IConfiguration configuration)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.MaxDepth = 10;
                options.JsonSerializerOptions.IncludeFields = true;
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.SuppressMapClientErrors = true;
            });

        services.AddAuthorization();
        services.AddScoped<ApiProvider>();
        services.RegisterOptionsOf<ApiDbOptions>(ApiDbOptions.SectionName);

        services
            .AddProblemDetails()
            .AddTimeProvider()
            .AddCaching()
            .AddHttpContextAccessor()
            .AddHttpLogging(options => { })
            .AddSwaggerConfiguration(configuration)
            .AddApiVersioningConfiguration()
            .AddDefaultExceptionHandlers()
            .WithCors()
            .AddForwardedHeadersConfiguration();

        return services;
    }
    private static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services, int majorVersion = ApiDefaultMajorVersion, string apiVersionGroupNameFormat = ApiVersionGroupNameFormat)
    {
        services.AddApiVersioning(configuration =>
        {
            configuration.DefaultApiVersion = new ApiVersion(majorVersion);
            configuration.AssumeDefaultVersionWhenUnspecified = true;
            configuration.ReportApiVersions = true;
            configuration.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader(ApiVersionHeader), new MediaTypeApiVersionReader(ApiVersionHeader));
        }).AddMvc(configuration =>
        {
            configuration.Conventions.Add(new VersionByNamespaceConvention());
        }).AddApiExplorer(configuration =>
        {
            configuration.GroupNameFormat = apiVersionGroupNameFormat;
            configuration.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
    private static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddMemoryCache();
        return services;
    }

    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.TryAddSingleton(TimeProvider.System);

        return services;
    }
    private static IServiceCollection AddDefaultExceptionHandlers(this IServiceCollection services)
    {
        return services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
    }

    private static IServiceCollection WithCors(
       this IServiceCollection services,
       string policy = DefaultCorsPolicy,
       Action<CorsPolicyBuilder>? configurePolicyBuilderDelegate = null)
    {

        services.AddCors(options =>
        {
            if (configurePolicyBuilderDelegate is null)
                options.AddPolicy(policy, builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            else
                options.AddPolicy(policy, configurePolicyBuilderDelegate);
        });

        return services;
    }

    private static IServiceCollection AddForwardedHeadersConfiguration(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return services;
    }

}