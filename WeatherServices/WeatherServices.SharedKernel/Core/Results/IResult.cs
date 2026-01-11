namespace WeatherServices.SharedKernel.Core.Results;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    Error? Error { get; }
    IReadOnlyDictionary<string, object> Metadata { get; }
    void WithError(Error error);
}

public interface IResult<T> : IResult
{
    T? ValueOrDefault { get; }
}