using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherServices.DatabaseManagement.Abstractions;
using WeatherServices.SharedKernel.Core.Domain;

namespace WeatherServices.DatabaseManagement.Internal;

internal sealed class DbFacade<TDbContext> : IDbFacade
where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public DbFacade(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();
        await dbSet.AddAsync(entity, cancellationToken);
    }

    public void Insert<TEntity>(TEntity entity)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();
        dbSet.Add(entity);
    }

    public async Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();
        await dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void BulkInsert<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();
        dbSet.AddRange(entities);
    }

    public void Update<TEntity>(TEntity entity)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();
        dbSet.Update(entity);
    }

    public void BulkUpdate<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();

        dbSet.UpdateRange(entities);
    }

    public void Delete<TEntity>(TEntity entity)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();

        dbSet.Remove(entity);
    }

    public void BulkDelete<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class
    {
        DbSet<TEntity> dbSet = CreateDbSet<TEntity>();

        dbSet.RemoveRange(entities);
    }

    public IQueryable<TEntity> Query<TEntity>(bool disableChangeTracker = false)
    where TEntity : class
    {
        if (!disableChangeTracker)
            return CreateDbSet<TEntity>().AsExpandableEFCore();

        return CreateDbSet<TEntity>()
            .AsNoTracking()
            .AsExpandableEFCore();
    }

    public void TrackAsUnchanged<TEntity>(TEntity entity) where TEntity : class
    {
        var visitedNodes = new HashSet<object>();

        _dbContext.ChangeTracker.TrackGraph(entity, node =>
        {
            PropertyEntry propertyEntry = node.Entry.Property(nameof(Entity.Id));

            if (propertyEntry.Metadata.IsPrimaryKey() && propertyEntry.CurrentValue is not null &&
                (int)propertyEntry.CurrentValue > 0)
            {
                if (visitedNodes.Add(node.Entry.Entity))
                {
                    if (node.Entry.State == EntityState.Detached)
                        node.Entry.State = EntityState.Unchanged;
                }
            }
        });
    }

    public IQueryable<TEntity> QueryRaw<TEntity>(
        string query,
        bool disableChangeTracker = false,
        params object[] parameters)
         where TEntity : class
    {
        if (!disableChangeTracker)
            return CreateDbSet<TEntity>().FromSqlRaw(query, parameters);

        return CreateDbSet<TEntity>()
            .FromSqlRaw(query, parameters)
            .AsNoTracking();
    }

    private DbSet<TEntity> CreateDbSet<TEntity>()
    where TEntity : class
        => _dbContext.Set<TEntity>();
}