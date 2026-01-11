using WeatherServices.Weather.Application.UseCases.DailyWeather;
using WeatherServices.Weather.Application.UseCases.ForecastWheater;

namespace WeatherServices.Weather.API.Controllers.v1;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.ProblemJson)]
public sealed class WeatherController : ControllerBase
{
    private readonly ISender _sender;
    public WeatherController(ISender sender) => _sender = sender;

    [HttpGet("current-daily")]
    public async Task<IActionResult> FindCurrentAsync(
       [FromQuery] DailyWeatherRequest request,
       CancellationToken cancellationToken)
    {
        Result<DailyWeatherResponse> result = await _sender.Send(request, cancellationToken);

        return this.ProcessResponse(result);
    }

    [HttpGet("weather-forecast")]
    public async Task<IActionResult> FindFiveDayForecastAsync(
       [FromQuery] ForecastWheaterRequest request,
       CancellationToken cancellationToken)
    {
        Result<ForecastWheaterResponse> result = await _sender.Send(request, cancellationToken);

        return this.ProcessResponse(result);
    }

}
