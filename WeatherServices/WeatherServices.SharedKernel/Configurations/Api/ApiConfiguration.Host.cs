using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace WeatherServices.SharedKernel.Configurations.Api;

public static partial class ApiConfiguration
{
    public static IWebHostBuilder AddKestrel(this IWebHostBuilder builder)
    {
        return builder.UseKestrel(options =>
        {
            options.ConfigureEndpointDefaults(defaultOptions =>
            {
                defaultOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });
    }

    public static WebApplicationBuilder AddConfigurationSources(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile(path: $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder;
    }
}
