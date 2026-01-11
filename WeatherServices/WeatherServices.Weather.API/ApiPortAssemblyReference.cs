using System.Reflection;

namespace WeatherServices.Weather.API;

public static class ApiPortAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ApiPortAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
