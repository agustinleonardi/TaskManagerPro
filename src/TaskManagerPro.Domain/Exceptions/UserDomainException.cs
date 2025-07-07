namespace TaskManagerPro.Domain.Exceptions;

/// <summary>
/// Excepción lanzada cuando se violan las reglas de negocio relacionadas con usuarios
/// </summary>
public class UserDomainException : DomainException
{
    public UserDomainException(string message) : base(message) { }

    public UserDomainException(string message, Exception exception) : base(message, exception) { }
}