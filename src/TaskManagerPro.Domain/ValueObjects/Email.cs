using System.ComponentModel.DataAnnotations;
using TaskManagerPro.Domain.Exceptions;

namespace TaskManagerPro.Domain.ValueObjects;

public readonly record struct Email
{
    public string Value { get; } = string.Empty;

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectException("El email no puede estar vacio");
        if (!IsValidEmail(value))
            throw new ValueObjectException($"El email '{value}' no tiene un formato valido");

        Value = value.ToLowerInvariant().Trim();
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // Operadores para facilitar el uso
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string value) => new(value);

    public override string ToString() => Value;
}