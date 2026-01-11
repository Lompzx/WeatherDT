using System.Reflection;

namespace WeatherServices.Identity;

public static class WeatherServicesAssemblyReference
{
    public static readonly Assembly Assembly = typeof(WeatherServicesAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
