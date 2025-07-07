using TaskManagerPro.Domain.Exceptions;

namespace TaskManagerPro.Domain.ValueObjects;

/// <summary>
/// Value Object que representa una descripción de tarea válida
/// Permite valores vacíos pero limita a máximo 1000 caracteres
/// </summary>
public readonly record struct TaskDescription
{
    public string Value { get; }

    public TaskDescription(string? value = null)
    {
        var trimmedValue = value?.Trim() ?? string.Empty;

        if (trimmedValue.Length > 1000)
            throw new ValueObjectException("La descripción de la tarea no puede exceder 1000 caracteres");

        Value = trimmedValue;
    }

    // Operadores para facilitar el uso
    public static implicit operator string(TaskDescription description) => description.Value;
    public static implicit operator TaskDescription(string? value) => new(value);

    public override string ToString() => Value;
}
