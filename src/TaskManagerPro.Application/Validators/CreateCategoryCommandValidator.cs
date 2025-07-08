using FluentValidation;
using TaskManagerPro.Application.Commands.Category;

namespace TaskManagerPro.Application.Validators;

/// <summary>
/// Validador para el comando de crear categoría
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryData)
            .NotNull()
            .WithMessage("Los datos de la categoría son requeridos");

        RuleFor(x => x.CategoryData.Name)
            .NotEmpty()
            .WithMessage("El nombre es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.CategoryData.Description)
            .MaximumLength(500)
            .WithMessage("La descripción no puede exceder 500 caracteres");

        RuleFor(x => x.CategoryData.Type)
            .IsInEnum()
            .WithMessage("El tipo debe ser un valor válido");
    }
}
