using System.Text.RegularExpressions;
using TaskManagerPro.Domain.Exceptions;

namespace TaskManagerPro.Domain.ValueObjects;

/// <summary>
/// Value Object que representa un nombre de usuario válido
/// Garantiza formato: 3-50 caracteres alfanuméricos y guión bajo
/// </summary>
public readonly record struct Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectException("El username no puede estar vacío");

        var trimmedValue = value.Trim();

        if (trimmedValue.Length < 3)
            throw new ValueObjectException("El username debe tener mínimo 3 caracteres");

        if (trimmedValue.Length > 50)
            throw new ValueObjectException("El username no puede exceder 50 caracteres");

        if (!Regex.IsMatch(trimmedValue, @"^[a-zA-Z0-9_]+$"))
            throw new ValueObjectException("El username solo puede contener letras, números y guión bajo");

        Value = trimmedValue;
    }

    // Operadores para facilitar el uso
    public static implicit operator string(Username username) => username.Value;
    public static implicit operator Username(string value) => new(value);

    public override string ToString() => Value;
}