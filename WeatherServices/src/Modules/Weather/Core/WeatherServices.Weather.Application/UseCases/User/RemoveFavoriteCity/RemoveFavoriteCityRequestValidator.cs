using FluentValidation;
using WeatherServices.SharedKernel.Configurations.FluentValidation;

namespace WeatherServices.Weather.Application.UseCases.User.RemoveFavoriteCity;

internal sealed class RemoveFavoriteCityRequestValidator : RequestValidator<RemoveFavoriteCityRequest>
{
    protected override void Validate()
    {
        RuleFor(request => request.Uuid)
            .NotEmpty()
            .WithMessage("Uuid is required");

        RuleFor(request => request.CityUuid)
            .NotEmpty()
            .WithMessage("City uuid is required");
    }
}
