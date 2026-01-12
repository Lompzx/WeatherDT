namespace WeatherServices.Weather.Application.UseCases.ForecastWheater;

public sealed class ForecastWheaterRequest :  IRequest<Result<ForecastWheaterResponse>>
{
    public required string Name { get; init; }
}
