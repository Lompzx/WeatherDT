using FluentValidation;
using WeatherServices.SharedKernel.Configurations.FluentValidation;

namespace WeatherServices.Weather.Application.UseCases.DailyWeather;

internal sealed class DailyWeatherRequestValidator : RequestValidator<DailyWeatherRequest>
{
    protected override void Validate()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("City name must be provided");
    }
}
