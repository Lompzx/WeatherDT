namespace WeatherServices.SharedKernel.Core.Results;

public class Result<T> : IResult<T>
{
    private readonly T? _value;
    private Success? _success;
    private Error? _error;
    private readonly Dictionary<string, object> _metadata = new();

    public Result() { } //For Where constraints only
    private Result(T value) => _value = value;
    private Result(T value, Success success)
    {
        _value = value;
        _success = success;
    }
    private Result(Error error) => _error = error;

    public bool IsSuccess => _error is null;
    public bool IsFailure => !IsSuccess;
    public Success? Success => _success;
    public Error? Error => _error;
    public IReadOnlyDictionary<string, object> Metadata => _metadata;
    public T? ValueOrDefault => _value;

    public void WithError(Error error)
    {
        _error = error;
    }

    public Result<T> WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public static Result<T> Ok(T value) => new Result<T>(value);
    public static Result<T> Ok(T value, Success success) => new Result<T>(value, success);
    public static Result<T> Fail(Error error) => new Result<T>(error);

    public static implicit operator Result<T>(Result result)
    {
        if (result is null)
            throw new ArgumentNullException(nameof(result));

        if (result.IsFailure && result.Error is not null)
            return Fail(result.Error);

        throw new InvalidOperationException("Cannot convert a successful non-generic Result to a generic Result<T> without a value. Use Ok<T>(value) instead.");
    }
}