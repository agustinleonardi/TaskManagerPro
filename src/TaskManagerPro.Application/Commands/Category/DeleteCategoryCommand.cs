using MediatR;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Comando para eliminar una categoría
/// </summary>
public class DeleteCategoryCommand : IRequest<bool>
{
    public Guid CategoryId { get; set; }

    public DeleteCategoryCommand(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public DeleteCategoryCommand()
    {
    }
}
