namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class Current
{
    public decimal Temp_c { get; init; }
    public decimal FeelsLike_c { get; init; }
    public int Humidity { get; init; }
    public Condition Condition { get; init; } = default!;
}
