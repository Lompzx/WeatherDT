namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class Location
{
    public string Name { get; init; } = default!;
    public string Region { get; init; } = default!;
    public string Country { get; init; } = default!;
    public decimal Lat { get; init; } = default!;
    public decimal Lon { get; init; } = default!;
    public string LocalTime { get; init; } = default!;

}
