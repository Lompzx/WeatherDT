using System.Data;

namespace WeatherServices.SharedKernel.Configurations.MediatR.Abstraction;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TransactionalAttribute : Attribute
{
    public IsolationLevel IsolationLevel { get; set; }

    public TransactionalAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        IsolationLevel = isolationLevel;
    }
}
