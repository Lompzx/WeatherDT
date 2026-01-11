namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs;

public sealed class DailyForecast
{
    public DateOnly Date { get; init; }
    public decimal MinTemp { get; init; }
    public decimal MaxTemp { get; init; }
    public required string Condition { get; init; }
    public required string Icon { get; init; } 

}
