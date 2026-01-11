namespace WeatherServices.Weather.Application.UseCases.ForecastWheater;

public sealed class ForecastWheaterResponse
{
    public required string City { get; init; } 
    public IReadOnlyCollection<DailyForecastResponse> Days { get; init; } = [];
}

public sealed class DailyForecastResponse
{
    public DateOnly Date { get; init; }
    public decimal MinTemp { get; init; }
    public decimal MaxTemp { get; init; }
    public required string Condition { get; init; } 
    public required string Icon { get; init; } 
}
