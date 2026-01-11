using FluentValidation;
using WeatherServices.SharedKernel.Configurations.FluentValidation;

namespace WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;

internal sealed class ListFavoriteCitiesRequestValidator : RequestValidator<ListFavoriteCitiesRequest>
{
    protected override void Validate()
    {
        RuleFor(request => request.Uuid)
           .NotEmpty()
           .WithMessage("User UUID must be provided");
    }
}
