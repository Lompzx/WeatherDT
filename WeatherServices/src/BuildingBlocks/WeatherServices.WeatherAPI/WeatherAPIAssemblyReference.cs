using System.Reflection;

namespace WeatherServices.WeatherAPI;

public static class WeatherAPIAssemblyReference
{
    public static readonly Assembly Assembly = typeof(WeatherAPIAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
