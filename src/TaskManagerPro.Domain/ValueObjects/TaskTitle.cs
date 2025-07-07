using TaskManagerPro.Domain.Exceptions;

namespace TaskManagerPro.Domain.ValueObjects;

/// <summary>
/// Value Object que representa un título de tarea válido
/// Garantiza que no esté vacío y tenga máximo 200 caracteres
/// </summary>
public readonly record struct TaskTitle
{
    public string Value { get; }

    public TaskTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectException("El título de la tarea no puede estar vacío");

        var trimmedValue = value.Trim();

        if (trimmedValue.Length > 200)
            throw new ValueObjectException("El título de la tarea no puede exceder 200 caracteres");

        Value = trimmedValue;
    }

    // Operadores para facilitar el uso
    public static implicit operator string(TaskTitle title) => title.Value;
    public static implicit operator TaskTitle(string value) => new(value);

    public override string ToString() => Value;
}
