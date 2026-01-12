using WeatherServices.WeatherAPI.Factories;
using WeatherServices.WeatherAPI.WeatherAPI.DTOs;

namespace WeatherServices.Weather.Application.UseCases.DailyWeather;

internal sealed class DailyWeatherRequestHandler : IRequestHandler<DailyWeatherRequest, Result<DailyWeatherResponse>>
{
    private const int daysQuantity = 1;
    private readonly IWeatherAPIFactory _weatherAPIFactory;    
    public DailyWeatherRequestHandler(IWeatherAPIFactory weatherAPIFactory)
    {
        _weatherAPIFactory = weatherAPIFactory;        
    }

    public async Task<Result<DailyWeatherResponse>> Handle(DailyWeatherRequest request, CancellationToken cancellationToken)
    {
        WeatherForecast? current = await _weatherAPIFactory.GetDaysForecastAsync(request.Name, daysQuantity, cancellationToken);

        if (current is null)
            return Result.NotFound("Weather data not found.");

        DailyWeatherResponse result = MapResponse(current);

        return Result.Ok(result);
    }

    private static DailyWeatherResponse MapResponse(WeatherForecast current)
    {
        return new DailyWeatherResponse
        {
            City = current.City,
            Temperature = current.Teperature,
            FeelsLike = current.Feelslike,
            Humidity = current.Humidity,
            Condition = current.Text,
            Icon = current.Icon,
            MinTemperature = current.Days.Single().MinTemp,
            MaxTemperature = current.Days.Single().MaxTemp
        };
    }
}
