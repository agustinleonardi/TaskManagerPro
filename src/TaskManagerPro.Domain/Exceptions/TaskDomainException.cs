namespace TaskManagerPro.Domain.Exceptions;

/// <summary>
/// Excepción lanzada cuando se violan las reglas de negocio relacionadas con tareas
/// </summary>
public class TaskDomainException : DomainException
{
    public TaskDomainException(string message) : base(message) { }

    public TaskDomainException(string message, Exception exception) : base(message, exception) { }
}