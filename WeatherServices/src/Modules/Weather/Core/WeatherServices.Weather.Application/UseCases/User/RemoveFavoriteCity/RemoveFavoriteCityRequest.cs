namespace WeatherServices.Weather.Application.UseCases.User.RemoveFavoriteCity;

public sealed class RemoveFavoriteCityRequest : IRequest<Result>
{
    public Guid Uuid { get; private set; }
    public Guid CityUuid { get; private set; }

    public RemoveFavoriteCityRequest(Guid uuid, Guid cityUuid)
    {
        Uuid = uuid;
        CityUuid = cityUuid;
    }
}
