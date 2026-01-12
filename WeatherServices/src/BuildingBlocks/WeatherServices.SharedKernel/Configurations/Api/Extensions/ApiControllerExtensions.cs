using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using WeatherServices.SharedKernel.Core.Results;

namespace WeatherServices.SharedKernel.Configurations.Api.Extensions;

public static class ApiControllerExtensions
{
    public static ObjectResult ProcessResponse(this ControllerBase controller, Result result, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return controller.StatusCode(StatusCodes.Status499ClientClosedRequest, null);

        if (result.IsFailure)
            return ProcessErrorResponse(controller, result.Error!);

        controller.TryAddCustomResponseHeaders(result);

        bool requestIsPost = controller.HttpContext.Request.Method == HttpMethod.Post.ToString();

        if (result.Success is not null)
            return controller.StatusCode(result.Success.Code, null);
        if (requestIsPost && controller.Response.Headers.ContainsKey(HeaderNames.Location))
            return controller.StatusCode(StatusCodes.Status201Created, new());
        if (requestIsPost)
            return controller.StatusCode(StatusCodes.Status200OK, new());

        return controller.StatusCode(StatusCodes.Status204NoContent, null);
    }

    public static ObjectResult ProcessResponse<TResponse>(this ControllerBase controller, Result<TResponse> result, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return controller.StatusCode(StatusCodes.Status499ClientClosedRequest, null);

        if (result.IsFailure)
            return ProcessErrorResponse(controller, result.Error!);

        controller.TryAddCustomResponseHeaders(result);       

        return controller.StatusCode(result.Success?.Code ?? StatusCodes.Status200OK, result.ValueOrDefault);
    }

    private static ObjectResult ProcessErrorResponse(this ControllerBase controller, Error error)
    {
        ValidationProblemDetails? problemDetails = controller.CreateProblemDetails(error);

        ArgumentNullException.ThrowIfNull(problemDetails);

        return controller.StatusCode(problemDetails!.Status!.Value, problemDetails);
    }
   

    private static ValidationProblemDetails? CreateProblemDetails(this ControllerBase controller, Error error)
    {
        string title = ReasonPhrases.GetReasonPhrase(error.Code);

        var problemDetails = new ValidationProblemDetails
        {
            Title = title,
            Detail = error.Message,
            Instance = controller.HttpContext.Request.Path,
            Status = error.Code,
            Type = error.Type
        };

        foreach (ErrorMetadata errorMetadata in error.Metadata)
            problemDetails.Errors.TryAdd(errorMetadata.Field, errorMetadata.Messages);

        return problemDetails;
    }

    private static void TryAddCustomResponseHeaders(this ControllerBase controller, Result result)
        => controller.AddCustomHeaders(result.Metadata);

    private static void TryAddCustomResponseHeaders<TResponse>(this ControllerBase controller, Result<TResponse> result)
        => controller.AddCustomHeaders(result.Metadata);

    private static void AddCustomHeaders(this ControllerBase controller, IReadOnlyDictionary<string, object> resultMetadata)
    {
        if (resultMetadata.Count > 0)
        {
            HttpResponse response = controller.HttpContext.Response;

            foreach (KeyValuePair<string, object> metadata in resultMetadata)
            {
                if (string.Equals(metadata.Key, HeaderNames.Location, StringComparison.OrdinalIgnoreCase))
                    response.Headers.TryAdd(HeaderNames.Location, $"{controller.BuildBaseUrlFromControllerContext()}/{metadata.Value}");
                else
                    response.Headers.TryAdd(metadata.Key, metadata.Value.ToString());
            }
        }
    }

    private static string BuildBaseUrlFromControllerContext(this ControllerBase controller)
    {
        const string routeVersionKey = "version";
        HttpRequest request = controller.HttpContext.Request;
        object version = request.RouteValues.Single(value => value.Key == routeVersionKey).Value!;
        
        string? pathBase = request.PathBase.Value;      

        return $"{pathBase}/api/v{version}/{controller.ControllerContext.ActionDescriptor.ControllerName}";
    }
}