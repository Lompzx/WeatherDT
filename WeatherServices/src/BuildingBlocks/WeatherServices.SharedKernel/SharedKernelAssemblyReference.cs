using System.Reflection;

namespace WeatherServices.SharedKernel;

public static class SharedKernelAssemblyReference
{
    public static readonly Assembly Assembly = typeof(SharedKernelAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}
