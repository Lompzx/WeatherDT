using Serilog;
using WeatherServices.Identity.IoC;
using WeatherServices.SharedKernel.Configurations.Api;
using WeatherServices.SharedKernel.Configurations.Serilog;
using WeatherServices.Weather.Application;

namespace WeatherServices.Weather.API;
public sealed class Program
{
    public static void Main(string[] args)
    {
        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Host.AddSerilogConfiguration();

            builder.WebHost.AddKestrel();

            builder.AddConfigurationSources();

            builder.Services.AddApiDefaultServices(builder.Configuration);
            builder.Services.AddApplicationModule(builder.Configuration);
            builder.Services.AddWeatherIdentity(builder.Configuration);            

            WebApplication app = builder.Build();

            app.UseApiDefaults(app.Environment, (customMiddlewareConfiguration) =>
            {
                //Set localization moddleware or other custom middleware here
            });

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.Information("Server Shutting down...");
            Log.CloseAndFlush();
        }   
    } 
}
