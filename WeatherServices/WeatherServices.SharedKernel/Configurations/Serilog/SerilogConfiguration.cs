using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace WeatherServices.SharedKernel.Configurations.Serilog;

public static class SerilogConfiguration
{
    private const string ConsoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";
    private const string EnvironmentVariableName = "DOTNET_ENVIRONMENT";
    private const string MicrosoftLoggerNamespace = "Microsoft.AspNetCore";

    public static IHostBuilder AddSerilogConfiguration(this IHostBuilder hostBuilder)
    {
        string applicationName = AppDomain.CurrentDomain.FriendlyName;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override(MicrosoftLoggerNamespace, LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithCorrelationId()
            .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable(EnvironmentVariableName)}")
            .WriteTo.Async(writer => writer.Console(outputTemplate: ConsoleOutputTemplate))
            .CreateLogger();

        hostBuilder.UseSerilog();

        return hostBuilder;
    }
}
