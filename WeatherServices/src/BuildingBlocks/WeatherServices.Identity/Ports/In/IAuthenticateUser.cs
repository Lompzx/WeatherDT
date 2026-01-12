using WeatherServices.Identity.Application.UseCases.AuthenticateUser;

namespace WeatherServices.Identity.Ports.In;

public interface IAuthenticateUser
{
    Task<AuthenticateUserResult> ExecuteAsync(
        AuthenticateUser command, CancellationToken cancellationToken);
}
