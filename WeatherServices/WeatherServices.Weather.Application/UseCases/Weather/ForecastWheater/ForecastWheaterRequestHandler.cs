using AutoMapper;
using WeatherServices.WeatherAPI.Factories;
using WeatherServices.WeatherAPI.WeatherAPI.DTOs;

namespace WeatherServices.Weather.Application.UseCases.ForecastWheater;

internal sealed class ForecastWheaterRequestHandler : IRequestHandler<ForecastWheaterRequest, Result<ForecastWheaterResponse>>
{
    private readonly IWeatherAPIFactory _weatherAPIFactory;
    private readonly IMapper _mapper;
    private const int daysQuantity = 6;

    public ForecastWheaterRequestHandler(IWeatherAPIFactory weatherAPIFactory, IMapper mapper)
    {
        _weatherAPIFactory = weatherAPIFactory;
        _mapper = mapper;   
    }

    public async Task<Result<ForecastWheaterResponse>> Handle(ForecastWheaterRequest request, CancellationToken cancellationToken)
    {
        WeatherForecast? forecast = await _weatherAPIFactory.GetDaysForecastAsync(request.Name, daysQuantity, cancellationToken);

        if (forecast is null)
            return Result.NotFound("Forecast data not found.");

        ForecastWheaterResponse response = _mapper.Map<ForecastWheaterResponse>(forecast);

        return Result.Ok(response);
    }
}
