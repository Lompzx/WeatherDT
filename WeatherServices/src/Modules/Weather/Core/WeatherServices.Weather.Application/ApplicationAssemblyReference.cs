using System.Reflection;

namespace WeatherServices.Weather.Application;

public static class ApplicationAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
