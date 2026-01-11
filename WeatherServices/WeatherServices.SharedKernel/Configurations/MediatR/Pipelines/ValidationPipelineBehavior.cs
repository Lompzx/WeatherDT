using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using WeatherServices.SharedKernel.Core.Results;
using IResult = WeatherServices.SharedKernel.Core.Results.IResult;

namespace WeatherServices.SharedKernel.Configurations.MediatR.Pipelines;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
where TResponse : IResult, new()
{
    private readonly IValidator<TRequest> _validator;
    public ValidationPipelineBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, options: options =>
        {
            options.IncludeAllRuleSets();
        }, cancellationToken);

        if (!validationResult.IsValid)
        {
            Error error = BuildError(validationResult.Errors);

            var invalidRequest = new TResponse();
            invalidRequest.WithError(error);

            return invalidRequest;
        }

        return await next();
    }


    private static Error BuildError(List<ValidationFailure> errors)
    {
        const string errorMessage = "One or more validation errors occurred";

        var error = new Error(StatusCodes.Status400BadRequest, errorMessage);

        if (errors?.Count > 0)
        {
            var errorMetadata = errors.GroupBy(error => error.PropertyName)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(e => e.ErrorMessage).ToArray());

            foreach (KeyValuePair<string, string[]> metadata in errorMetadata)
                error.AddMetadata(metadata.Key, metadata.Value);
        }

        return error;
    }
}