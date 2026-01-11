using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeatherServices.DatabaseManagement.Abstractions;
using WeatherServices.Weather.Domain.User;

namespace WeatherServices.Weather.SqlServer.EntityFramework.Weather;

public static class WeatherQueryableExtensions 
{
    public static IQueryable<Users> User(this IDbFacade dbFacade, bool disableChangeTracker = false) => dbFacade.Query<Users>(disableChangeTracker);

    public static IIncludableQueryable<Users, IEnumerable<UserFavoriteCity>> WithFavorites(
        this IQueryable<Users> source,
        Expression<Func<UserFavoriteCity, bool>>? predicate = null)
        => predicate is not null 
        ? source.Include(user => user.FavoriteCities.Where(predicate.Compile()))
        : source.Include(user => user.FavoriteCities);
}
