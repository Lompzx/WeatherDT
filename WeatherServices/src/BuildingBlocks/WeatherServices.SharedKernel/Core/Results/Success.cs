namespace WeatherServices.SharedKernel.Core.Results;

public sealed class Success
{
    public int Code { get; init; }

    public Success(int code)
    {
        Code = code;
    }
}