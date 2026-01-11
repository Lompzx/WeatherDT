namespace WeatherServices.Identity.Application.UseCases.AuthenticateUser;

public sealed class AuthenticateUser
{
    public string Username { get; }
    public string Password { get; }

    public AuthenticateUser(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
