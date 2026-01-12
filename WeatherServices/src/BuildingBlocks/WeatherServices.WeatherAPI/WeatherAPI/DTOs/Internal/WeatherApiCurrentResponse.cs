namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class WeatherApiCurrentResponse
{
    public Location Location { get; init; } = default!;
    public Current Current { get; init; } = default!;
}
