namespace WeatherServices.Identity.Application.UseCases.AuthenticateUser;

public sealed class AuthenticateUserResult
{
    public Guid UserId { get; }
    public string Username { get; }
    public string AccessToken { get; }

    public AuthenticateUserResult(
        Guid userId,
        string username,
        string accessToken)
    {
        UserId = userId;
        Username = username;
        AccessToken = accessToken;
    }
}
