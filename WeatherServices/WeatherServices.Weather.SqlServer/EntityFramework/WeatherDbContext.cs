using Microsoft.EntityFrameworkCore;
using WeatherServices.SharedKernel.Core.Domain;
using WeatherServices.Weather.Domain;

namespace WeatherServices.Weather.SqlServer.EntityFramework;

internal sealed class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IReadOnlyList<Type> domainTypes = DomainAssemblyReference.Assembly
               .GetTypes()
               .Where(type => type.IsSubclassOf(typeof(Entity)))
               .ToList();

        foreach (Type type in domainTypes)
        {
            modelBuilder.Entity(type);                       
        }

        base.OnModelCreating(modelBuilder);
    }
}
