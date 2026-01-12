namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class WeatherApiForecastResponse
{
    public Current Current { get; init; } = default!;
    public Location Location { get; init; } = default!;
    public Forecast Forecast { get; init; } = default!;
}
