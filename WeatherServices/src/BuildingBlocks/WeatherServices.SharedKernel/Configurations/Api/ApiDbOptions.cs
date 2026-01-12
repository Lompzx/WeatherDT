namespace WeatherServices.SharedKernel.Configurations.Api;

public sealed class ApiDbOptions
{
    public const string SectionName = "DbSettings";
    public required ApiDbSettings Setting { get; init; }
}

public sealed class ApiDbSettings
{
    public required string ConnectionString { get; init; }
}
