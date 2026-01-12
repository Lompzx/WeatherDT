namespace WeatherServices.SharedKernel.Core.Results;

public interface IHttpError
{
    static abstract Result BadRequest(string message);
    static abstract Result BadRequest(Error error);
    static abstract Result Unauthorized(string message);
    static abstract Result PaymentRequired(string message);
    static abstract Result Forbidden(string message);
    static abstract Result NotFound(string message);
    static abstract Result MethodNotAllowed(string message);
    static abstract Result NotAcceptable(string message);
    static abstract Result ProxyAuthenticationRequired(string message);
    static abstract Result RequestTimeout(string message);
    static abstract Result Conflict(string message);
    static abstract Result Gone(string messageages);
    static abstract Result LengthRequired(string message);
    static abstract Result PreConditionFailed(string message);
    static abstract Result PayloadTooLarge(string message);
    static abstract Result UriTooLong(string message);
    static abstract Result UnsupportedMediaType(string message);
    static abstract Result UnprocessableEntity(string message);
    static abstract Result FailedDependency(string message);
    static abstract Result TooManyRequests(string message);
    static abstract Result UnavailableForLegalReasons(string message);
    static abstract Result ClientClosedRequest(string messages);
    static abstract Result InternalServerError(string message);
    static abstract Result NotImplemented(string message);
    static abstract Result BadGateway(string message);
    static abstract Result ServiceUnavailable(string message);
    static abstract Result GatewayTimeout(string message);
    static abstract Result HttpVersionNotSupported(string message);
    static abstract Result NetworkAuthenticationRequired(string message);
}