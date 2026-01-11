using WeatherServices.Identity.Domain.Entities;

namespace WeatherServices.Identity.Ports.Out;

public interface IUserRepository
{
    Task<Users?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}
