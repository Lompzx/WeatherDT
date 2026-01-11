using WeatherServices.Identity.Domain.Interfaces;

namespace WeatherServices.Identity.Infraestructure.Security.Hash;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}