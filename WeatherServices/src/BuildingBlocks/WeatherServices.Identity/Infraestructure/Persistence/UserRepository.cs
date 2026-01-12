using Dapper;
using System.Data;
using WeatherServices.DatabaseManagement.Abstractions;
using WeatherServices.Identity.Domain.Entities;
using WeatherServices.Identity.Ports.Out;

namespace WeatherServices.Identity.Infraestructure.Persistence;

internal sealed class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public UserRepository(IDbConnectionFactory dbConnectionFactory) => _dbConnectionFactory = dbConnectionFactory;
    
    public async Task<Users?> GetByUsernameAsync(string email, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateAsync(cancellationToken);

        string query = @"SELECT * FROM Users 
                       WHERE Email = @Email";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(nameof(email), email);

        Users? user = await connection.QueryFirstOrDefaultAsync<Users>(query, dynamicParameters);

        return user;
    }
}
