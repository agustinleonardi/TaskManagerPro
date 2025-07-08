using FluentValidation;
using TaskManagerPro.Application.Commands.Task;

namespace TaskManagerPro.Application.Validators;

/// <summary>
/// Validador para el comando de crear tarea
/// </summary>
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.TaskData)
            .NotNull()
            .WithMessage("Los datos de la tarea son requeridos");

        RuleFor(x => x.TaskData.Title)
            .NotEmpty()
            .WithMessage("El título es requerido")
            .MaximumLength(200)
            .WithMessage("El título no puede exceder 200 caracteres");

        RuleFor(x => x.TaskData.Description)
            .MaximumLength(1000)
            .WithMessage("La descripción no puede exceder 1000 caracteres");

        RuleFor(x => x.TaskData.UserId)
            .NotEmpty()
            .WithMessage("El ID del usuario es requerido");

        RuleFor(x => x.TaskData.Priority)
            .IsInEnum()
            .WithMessage("La prioridad debe ser un valor válido");
    }
}
