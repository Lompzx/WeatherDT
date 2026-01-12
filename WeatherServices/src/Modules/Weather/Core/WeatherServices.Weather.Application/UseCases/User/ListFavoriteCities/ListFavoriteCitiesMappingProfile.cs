using WeatherServices.SharedKernel.Configurations.AutoMapper;
using WeatherServices.Weather.Domain.User;

namespace WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;

internal class ListFavoriteCitiesMappingProfile : ApplicationMappingProfile
{
    protected override void RegisterMappings()
    {
        CreateMap<Users, ListFavoriteCitiesResponse>();
        CreateMap<UserFavoriteCity, UserFavoriteCityResponse>();
    }
}
