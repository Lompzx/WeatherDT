using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace WeatherServices.SharedKernel.Configurations.Middlewares.FactoryBased;

internal sealed class GlobalExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        int httpStatusCode = (int)HttpStatusCode.InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Title = ReasonPhrases.GetReasonPhrase(httpStatusCode),
            Detail = "An error occurred while processing your request",
            Status = httpStatusCode,
            Instance = httpContext.Request.Path.Value
        };

        string serializedProblemDetails = JsonSerializer.Serialize(problemDetails);

        _logger.LogError("An error occurred: {ErrorMessage}", exception.Message);

        httpContext.Response.StatusCode = httpStatusCode;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsync(serializedProblemDetails, cancellationToken: cancellationToken);

        return httpContext.Response.HasStarted;        
    }
}
