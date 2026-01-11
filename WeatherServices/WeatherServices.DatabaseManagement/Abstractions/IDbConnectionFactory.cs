using System.Data;

namespace WeatherServices.DatabaseManagement.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync(CancellationToken cancellationToken = default);
}
