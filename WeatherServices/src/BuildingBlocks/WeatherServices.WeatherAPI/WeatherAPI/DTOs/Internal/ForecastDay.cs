namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class ForecastDay
{
    public string Date { get; init; } = default!;
    public Day Day { get; init; } = default!;
}
