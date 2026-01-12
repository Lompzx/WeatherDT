using WeatherServices.SharedKernel.Configurations.AutoMapper;
using WeatherServices.Weather.Domain.User;

namespace WeatherServices.Weather.Application.UseCases.User.AddOrEditFavoriteCity;

internal sealed class AddFavoriteCityMappingProfile : ApplicationMappingProfile
{
    protected override void RegisterMappings()
    {
        CreateMap<FavoriteCityRequest, UserFavoriteCity>()
            .ForMember(destination => destination.Uuid, options => options.MapFrom(source => SetUuid(source)));
    }
    private static Guid SetUuid(FavoriteCityRequest source)
    {
        if (!source.Uuid.HasValue)
            return Guid.NewGuid();

        return source.Uuid.Value;
    }
}
