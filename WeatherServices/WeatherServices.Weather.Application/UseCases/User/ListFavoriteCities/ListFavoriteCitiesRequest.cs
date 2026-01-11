namespace WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;

public sealed class ListFavoriteCitiesRequest : IRequest<Result<ListFavoriteCitiesResponse>>
{
    public Guid Uuid { get; private set; }

    public ListFavoriteCitiesRequest(Guid uuid) => Uuid = uuid;    
}
