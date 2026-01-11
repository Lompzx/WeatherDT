namespace WeatherServices.Weather.Application.UseCases.DailyWeather;

public sealed class DailyWeatherRequest : IRequest<Result<DailyWeatherResponse>>
{
    public required string Name { get; init; }
}
