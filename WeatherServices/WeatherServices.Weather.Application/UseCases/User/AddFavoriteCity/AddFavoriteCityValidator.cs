using FluentValidation;
using WeatherServices.SharedKernel.Configurations.FluentValidation;

namespace WeatherServices.Weather.Application.UseCases.User.AddOrEditFavoriteCity;

internal sealed class AddFavoriteCityValidator : RequestValidator<AddFavoriteCityRequest>
{
    protected override void Validate()
    {
        RuleFor(request => request.Uuid)
            .NotEmpty()
            .WithMessage("User UUID must be provided");

        RuleFor(request => request.City.Name)
            .NotEmpty()
            .WithMessage("City name must be provided");
    }
}
