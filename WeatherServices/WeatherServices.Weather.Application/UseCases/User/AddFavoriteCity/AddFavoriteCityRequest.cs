namespace WeatherServices.Weather.Application.UseCases.User.AddOrEditFavoriteCity;

public sealed class AddFavoriteCityRequest : IRequest<Result>
{
    public Guid Uuid { get; private set; }

    public required FavoriteCityRequest City { get; init; }

    public void AttatchUuid(Guid uuid) => Uuid = uuid;
}

public sealed class FavoriteCityRequest
{
    public Guid? Uuid { get; init; }
    public required string Name { get; init; }
}
