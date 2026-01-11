using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;
using WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;
using WeatherServices.SharedKernel.Core.Results;

namespace WeatherServices.SharedKernel.Configurations.MediatR.Pipelines;

internal sealed class DbTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    private readonly ITransactionalPipelineBehavior _transactionManager;
    private readonly ILogger<DbTransactionPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IsolationLevel _isolationLevel;
    private readonly bool _hasNonTransactionalAttribute;
    public DbTransactionPipelineBehavior(
        ITransactionalPipelineBehavior transactionManager,
        ILogger<DbTransactionPipelineBehavior<TRequest, TResponse>> logger)
    {
        Type requestType = typeof(TRequest);

        _hasNonTransactionalAttribute = Attribute.IsDefined(requestType, typeof(NonTransactionalAttribute));
        bool hasTransactionalAttribute = Attribute.IsDefined(requestType, typeof(TransactionalAttribute));

        if (_hasNonTransactionalAttribute && hasTransactionalAttribute)
            throw new InvalidOperationException($"The request {requestType.Name} cannot have both [Transactional] and [NonTransactional] attributes applied simultaneously.");

        _transactionManager = transactionManager;
        _logger = logger;
        _isolationLevel = GetIsolationLevel(requestType);
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_hasNonTransactionalAttribute)
            return await next();

        await using IDbContextTransaction transaction = await _transactionManager.BeginTransactionAsync(cancellationToken, _isolationLevel);

        try
        {
            await RegisterRollbackCallbackAsync(request, transaction, cancellationToken);

            TResponse response = await next();

            if (response is IResult result)
            {
                if (result.IsSuccess)
                {
                    await _transactionManager.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "An error occurred during database transaction.");

            throw;
        }
    }

    private Task RegisterRollbackCallbackAsync(TRequest request, IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        cancellationToken.Register(async () =>
        {
            string requestType = request.GetType().Name;

            _logger.LogWarning(
                 "The request has been canceled for request of type: {RequestType} with the following request body: {RequestBody}",
                 requestType,
                 request);

            await transaction.RollbackAsync(cancellationToken);
        });

        return Task.CompletedTask;
    }

    private IsolationLevel GetIsolationLevel(Type requestType)
    {
        var transactionalAttribute = Attribute.GetCustomAttribute(requestType, typeof(TransactionalAttribute)) as TransactionalAttribute;

        if (transactionalAttribute is not null)
            return transactionalAttribute.IsolationLevel;

        return IsolationLevel.ReadCommitted;
    }

}
