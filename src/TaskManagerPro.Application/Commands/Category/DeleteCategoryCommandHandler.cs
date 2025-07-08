using MediatR;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Handler para procesar el comando de eliminar categoría
/// </summary>
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        // Verificar que la categoría existe
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new ArgumentException($"Categoría con ID {request.CategoryId} no encontrada");

        // Eliminar la categoría
        await _categoryRepository.DeleteAsync(category);

        return true;
    }
}
