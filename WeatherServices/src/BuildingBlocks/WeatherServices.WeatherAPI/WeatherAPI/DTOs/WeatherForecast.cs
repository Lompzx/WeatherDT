namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs;

public sealed class WeatherForecast
{
    public required string City { get; init; } 
    public decimal Teperature { get; init; }
    public decimal Feelslike { get; init; }
    public int Humidity { get; init; }
    public required string Text { get; init; }
    public required string Icon { get; init; }

    public IReadOnlyCollection<DailyForecast> Days { get; init; } = [];
}
