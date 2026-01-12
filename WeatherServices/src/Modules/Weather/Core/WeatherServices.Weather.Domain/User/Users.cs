using WeatherServices.SharedKernel.Core.Domain;
using WeatherServices.SharedKernel.Core.Extensions;

namespace WeatherServices.Weather.Domain.User;

public sealed class Users : AggregateRoot
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public IReadOnlyCollection<UserFavoriteCity> FavoriteCities => _favoriteCities!;

    private readonly HashSet<UserFavoriteCity>? _favoriteCities = new();

    public void AddFavoriteCity(UserFavoriteCity favoriteCity)
    {
        favoriteCity.WithUserId(this.Id);
        _favoriteCities!.AddIfNotNull(favoriteCity);
    }

    public void RemoveFavoriteCity(UserFavoriteCity favoriteCity)
    {
        UserFavoriteCity? userFavoriteCity = FavoriteCities.FirstOrDefault(city => city.Name.Equals(favoriteCity.Name, StringComparison.OrdinalIgnoreCase));
       
        if (userFavoriteCity is not null)
            _favoriteCities!.Remove(userFavoriteCity);
    }
}
