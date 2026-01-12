namespace WeatherServices.WeatherAPI.Weather;

public sealed class WeatherOptions
{
    public const string SectionName = "WeatherAPI";   
    
    public required string ApiKey { get; init; }
}
