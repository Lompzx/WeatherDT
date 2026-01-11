using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;

namespace WeatherServices.DatabaseManagement.EntityFramework.Pipeline.MediatR;

internal sealed class TransactionalPipelineBehavior<TDbContext> : ITransactionalPipelineBehavior
where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private IDbContextTransaction _transaction = null!;

    public TransactionalPipelineBehavior(TDbContext dbContext) => _dbContext = dbContext;

    public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => _transaction = _dbContext.Database.BeginTransaction(isolationLevel);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => _transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

    public int SaveChanges() => _dbContext.SaveChanges();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _dbContext.SaveChangesAsync(cancellationToken);

    public void Commit() => _transaction.Commit();

    public async Task CommitAsync(CancellationToken cancellationToken) => await _transaction.CommitAsync(cancellationToken);

    public void Rollback() => _transaction.Rollback();

    public Task RollbackAsync(CancellationToken cancellationToken) => _transaction.RollbackAsync(cancellationToken);
}