using WeatherServices.Identity.Domain.Entities;

namespace WeatherServices.Identity.Ports.Out;

public interface ITokenService
{
    string GenerateAccessToken(Users user);
}
