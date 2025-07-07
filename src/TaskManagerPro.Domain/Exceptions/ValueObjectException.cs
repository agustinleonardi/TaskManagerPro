namespace TaskManagerPro.Domain.Exceptions;

public class ValueObjectException : DomainException
{
    public ValueObjectException(string message) : base(message)
    {
    }

    public ValueObjectException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
