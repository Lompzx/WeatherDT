using FluentValidation;
using WeatherServices.SharedKernel.Configurations.FluentValidation;

namespace WeatherServices.Weather.Application.UseCases.ForecastWheater;

internal sealed class ForecastWheaterRequestValidator : RequestValidator<ForecastWheaterRequest>
{
    protected override void Validate()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("City must be provided.");            
    }
}
