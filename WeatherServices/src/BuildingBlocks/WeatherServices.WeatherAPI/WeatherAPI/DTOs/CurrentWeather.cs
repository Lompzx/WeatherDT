namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs;

public sealed class CurrentWeather
{
    public required string City { get; init; }
    public required string Region { get; init; }
    public required string Country { get; init; }
    public required decimal TemperatureC { get; init; }
    public required decimal FeelsLikeC { get; init; }
    public int Humidity { get; init; }
    public required string Condition { get; init; }
    public required string Icon { get; init; } 
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
    public required string LocalTime { get; init; } 
}
