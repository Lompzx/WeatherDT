namespace WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

internal sealed class Forecast
{
    public List<ForecastDay> ForecastDay { get; init; } = [];
}
