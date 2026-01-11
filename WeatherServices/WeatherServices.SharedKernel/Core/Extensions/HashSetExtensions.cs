namespace WeatherServices.SharedKernel.Core.Extensions;

public static class HashSetExtensions
{
    public static bool AddIfNotNull<T>(this HashSet<T?> hashSet, T? item)
    where T : class
    {
        if (item is null)
            return false;

        return hashSet.Add(item);
    }
}
