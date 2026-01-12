namespace WeatherServices.DatabaseManagement.Abstractions;

public interface IDbFacade
{
    Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
    where TEntity : class;

    void Insert<TEntity>(TEntity entity)
    where TEntity : class;
    Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    where TEntity : class;

    void BulkInsert<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class;

    void Update<TEntity>(TEntity entity)
    where TEntity : class;

    void BulkUpdate<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class;

    void Delete<TEntity>(TEntity entity)
    where TEntity : class;

    void BulkDelete<TEntity>(IEnumerable<TEntity> entities)
    where TEntity : class;

    IQueryable<TEntity> Query<TEntity>(bool disableChangeTracker = false)
    where TEntity : class;

    IQueryable<TEntity> QueryRaw<TEntity>(string query, bool disableChangeTracker = false, params object[] parameters)
        where TEntity : class;

    void TrackAsUnchanged<TEntity>(TEntity entity)
    where TEntity : class;
}