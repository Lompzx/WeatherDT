using System.Reflection;

namespace WeatherServices.Weather.Domain;

public static class DomainAssemblyReference
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
