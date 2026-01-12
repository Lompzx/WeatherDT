using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using WeatherServices.DatabaseManagement.Abstractions;
using WeatherServices.SharedKernel.Configurations.Api;

namespace WeatherServices.DatabaseManagement.Internal.Factories;

internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IOptions<ApiDbOptions> _options;    
    public DbConnectionFactory(IOptions<ApiDbOptions> options)
    {
        _options = options;
    }
    public async Task<IDbConnection> CreateAsync(CancellationToken cancellationToken = default)
    {  
        return await CreateSqlConnectionAsync(_options.Value.Setting.ConnectionString, cancellationToken);
    }
    private async Task<SqlConnection> CreateSqlConnectionAsync(string connectionString, CancellationToken cancellationToken)
    {
        var connection = new SqlConnection(connectionString);

        await connection.OpenAsync(cancellationToken);        

        return connection;
    }
}
