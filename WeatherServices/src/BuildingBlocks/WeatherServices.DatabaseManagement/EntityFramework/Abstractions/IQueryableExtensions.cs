using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WeatherServices.DatabaseManagement.EntityFramework.Abstractions;

public static class IQueryableExtensions
{
    public static async Task<T?> FindOneAsync<T>(
        this IQueryable<T> source,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        where T : class
    {
        List<T> result = await source.Where(predicate).ToListAsync(cancellationToken);

        return result.SingleOrDefault();
    }
}
