namespace WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;

public sealed class ListFavoriteCitiesResponse
{
    public Guid Uuid { get; init; }
    public required string Name { get; init; }    
    public required IReadOnlyList<UserFavoriteCityResponse> FavoriteCities { get; init; }
}

public class UserFavoriteCityResponse
{
    public Guid Uuid { get; init; }
    public required string Name { get; init; }
}