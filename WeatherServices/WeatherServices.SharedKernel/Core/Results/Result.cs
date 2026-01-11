using Microsoft.AspNetCore.Http;

namespace WeatherServices.SharedKernel.Core.Results;

public class Result : IResult, IHttpError, IHttpRedirect
{
    private Success? _success;
    private Error? _error;
    private readonly Dictionary<string, object> _metadata = new();

    public Result() { } //For Where constraints only
    private Result(Error error) => _error = error;
    private Result(Success success) => _success = success;

    public bool IsSuccess => _error is null;
    public bool IsFailure => !IsSuccess;
    public Success? Success => _success;
    public Error? Error => _error;
    public IReadOnlyDictionary<string, object> Metadata => _metadata;

    public void WithError(Error error)
    {
        _error = error;
    }

    public Result WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public static Result Ok() => new Result();
    public static Result<T> Ok<T>(T value) => Result<T>.Ok(value);
    public static Result<T> Ok<T>(T value, Success success) => Result<T>.Ok(value, success);
    public static Result Fail(Error error) => new Result(error);
    public static Result Fail(int statusCode, string? message) => Fail(new Error(statusCode, message ?? string.Empty));

    public static Result MultipleChoices() => new Result(new Success(StatusCodes.Status300MultipleChoices));
    public static Result MovedPermanently() => new Result(new Success(StatusCodes.Status301MovedPermanently));
    public static Result SeeOther() => new Result(new Success(StatusCodes.Status303SeeOther));
    public static Result NotModified() => new Result(new Success(StatusCodes.Status304NotModified));
    public static Result TemporaryRedirect() => new Result(new Success(StatusCodes.Status307TemporaryRedirect));
    public static Result PermanentRedirect() => new Result(new Success(StatusCodes.Status308PermanentRedirect));

    public static Result BadRequest(string message) => Fail(new Error(StatusCodes.Status400BadRequest, message));
    public static Result BadRequest(Error error) => Fail(error);
    public static Result Unauthorized(string message) => Fail(new Error(StatusCodes.Status400BadRequest, message));
    public static Result PaymentRequired(string message) => Fail(new Error(StatusCodes.Status402PaymentRequired, message));
    public static Result Forbidden(string message) => Fail(new Error(StatusCodes.Status403Forbidden, message));
    public static Result NotFound(string message) => Fail(new Error(StatusCodes.Status404NotFound, message));
    public static Result MethodNotAllowed(string message) => Fail(new Error(StatusCodes.Status405MethodNotAllowed, message));
    public static Result NotAcceptable(string message) => Fail(new Error(StatusCodes.Status406NotAcceptable, message));
    public static Result ProxyAuthenticationRequired(string message) => Fail(new Error(StatusCodes.Status407ProxyAuthenticationRequired, message));
    public static Result RequestTimeout(string message) => Fail(new Error(StatusCodes.Status408RequestTimeout, message));
    public static Result Conflict(string message) => Fail(new Error(StatusCodes.Status409Conflict, message));
    public static Result Gone(string message) => Fail(new Error(StatusCodes.Status410Gone, message));
    public static Result LengthRequired(string message) => Fail(new Error(StatusCodes.Status411LengthRequired, message));
    public static Result PreConditionFailed(string message) => Fail(new Error(StatusCodes.Status412PreconditionFailed, message));
    public static Result PayloadTooLarge(string message) => Fail(new Error(StatusCodes.Status413PayloadTooLarge, message));
    public static Result UriTooLong(string message) => Fail(new Error(StatusCodes.Status414UriTooLong, message));
    public static Result UnsupportedMediaType(string message) => Fail(new Error(StatusCodes.Status415UnsupportedMediaType, message));
    public static Result UnprocessableEntity(string message) => Fail(new Error(StatusCodes.Status422UnprocessableEntity, message));
    public static Result FailedDependency(string message) => Fail(new Error(StatusCodes.Status424FailedDependency, message));
    public static Result TooManyRequests(string message) => Fail(new Error(StatusCodes.Status429TooManyRequests, message));
    public static Result UnavailableForLegalReasons(string message) => Fail(new Error(StatusCodes.Status451UnavailableForLegalReasons, message));
    public static Result ClientClosedRequest(string message) => Fail(new Error(StatusCodes.Status499ClientClosedRequest, message));
    public static Result InternalServerError(string message) => Fail(new Error(StatusCodes.Status500InternalServerError, message));
    public static Result NotImplemented(string message) => Fail(new Error(StatusCodes.Status501NotImplemented, message));
    public static Result BadGateway(string message) => Fail(new Error(StatusCodes.Status502BadGateway, message));
    public static Result ServiceUnavailable(string message) => Fail(new Error(StatusCodes.Status503ServiceUnavailable, message));
    public static Result GatewayTimeout(string message) => Fail(new Error(StatusCodes.Status504GatewayTimeout, message));
    public static Result HttpVersionNotSupported(string message) => Fail(new Error(StatusCodes.Status505HttpVersionNotsupported, message));
    public static Result NetworkAuthenticationRequired(string message) => Fail(new Error(StatusCodes.Status511NetworkAuthenticationRequired, message));
}