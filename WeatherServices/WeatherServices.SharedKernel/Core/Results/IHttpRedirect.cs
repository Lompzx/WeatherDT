namespace WeatherServices.SharedKernel.Core.Results;

public interface IHttpRedirect
{
    static abstract Result MultipleChoices();
    static abstract Result MovedPermanently();
    static abstract Result SeeOther();
    static abstract Result NotModified();
    static abstract Result TemporaryRedirect();
    static abstract Result PermanentRedirect();
}
