using FluentValidation;
using TaskManagerPro.Application.Commands.User;

namespace TaskManagerPro.Application.Validators;

/// <summary>
/// Validador para el comando CreateUser
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        // Validar que UserData no sea null
        RuleFor(x => x.UserData)
            .NotNull()
            .WithMessage("Los datos del usuario son requeridos");

        // Cuando UserData no es null, validar sus propiedades
        When(x => x.UserData != null, () =>
        {

            //Validacion del email
            RuleFor(x => x.UserData.Email)
                .NotEmpty()
                .WithMessage("El email es requerido")
                .EmailAddress()
                .WithMessage("El email debe tener un formato valido")
                .MaximumLength(100)
                .WithMessage("El email no puede exceder 100 caracteres");

            //Validacion del Username
            RuleFor(x => x.UserData.Username)
                .NotEmpty()
                .WithMessage("El nombre de usuario es requerido")
                .MinimumLength(3)
                .WithMessage("El nombre de usuario debe tener al menos 3 caracteres")
                .MaximumLength(50)
                .WithMessage("El nombre de usuario no puede exceder los 50 caracteres")
                .Matches("^[a-zA-Z0-9_]+$")
                .WithMessage("El nombre de usuario solo puede contener letras, numeros y guiones bajos");

            //Validacion del Password
            RuleFor(x => x.UserData.Password)
                .NotEmpty()
                .WithMessage("La contraseña es requerida")
                .MinimumLength(6)
                .WithMessage("La contraseña debe tener al menos 6 caracteres")
                .MaximumLength(20)
                .WithMessage("La contraseña no puede exceder los 20 caracteres");
        });
    }
}
