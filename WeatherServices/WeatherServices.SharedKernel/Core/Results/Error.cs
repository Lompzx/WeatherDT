namespace WeatherServices.SharedKernel.Core.Results;

public sealed class Error
{
    public int Code { get; init; }
    public string Message { get; init; }
    public string? Type { get; init; }

    public IReadOnlyList<ErrorMetadata> Metadata => _metadata;

    private readonly List<ErrorMetadata> _metadata = new();

    public Error(int code, string message, string? type = null)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public void AddMetadata(string field, params string[] messages) => _metadata.Add(new ErrorMetadata { Field = field, Messages = messages });
}

public sealed class ErrorMetadata
{
    public required string Field { get; init; }
    public required string[] Messages { get; init; }
}
