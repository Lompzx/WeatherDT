using WeatherServices.SharedKernel.Core.Domain;

namespace WeatherServices.Weather.Domain.User;

public sealed class UserFavoriteCity : Entity
{    
    public int UsersId { get; private set; }
    public string Name { get; private set; } = default!;

    public void Edit(UserFavoriteCity favoriteCity)
    {
        Name = favoriteCity.Name ?? Name;
    }

    //BAD for real case, association it has to be automatically
    public void WithUserId(int userId) => UsersId = userId;
}
