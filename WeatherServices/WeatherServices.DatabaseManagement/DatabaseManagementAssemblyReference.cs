using System.Reflection;

namespace WeatherServices.DatabaseManagement;

public static class DatabaseManagementAssemblyReference
{
    public static readonly Assembly Assembly = typeof(DatabaseManagementAssemblyReference).Assembly;
    public static readonly string? Name = Assembly.GetName().Name;
}

