using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;


public interface ITransactionalPipelineBehavior
{
    IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void Commit();
    Task CommitAsync(CancellationToken cancellationToken);
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken);
}