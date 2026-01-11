namespace WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;

[AttributeUsage(AttributeTargets.Class)]
public sealed class NonTransactionalAttribute : Attribute
{

}
