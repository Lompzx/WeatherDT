namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class Day
{
    public decimal MinTemp_c { get; init; }
    public decimal MaxTemp_c { get; init; }
    public Condition Condition { get; init; } = default!;
}
