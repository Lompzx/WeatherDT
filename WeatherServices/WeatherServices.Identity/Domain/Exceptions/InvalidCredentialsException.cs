namespace WeatherServices.Identity.Domain.Exceptions;

internal sealed class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid username or password.")
    {
    }
}
