using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Category;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Handler para procesar el comando de actualizar categoría
/// </summary>
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponseDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Obtener la categoría existente
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new ArgumentException($"Categoría con ID {request.CategoryId} no encontrada");

        // Actualizar propiedades
        if (!string.IsNullOrWhiteSpace(request.CategoryData.Name))
            category.ChangeName(request.CategoryData.Name);

        if (request.CategoryData.Description != null)
            category.ChangeDescription(request.CategoryData.Description);

        if (request.CategoryData.Type.HasValue)
            category.ChangeType(request.CategoryData.Type.Value);

        // Guardar cambios
        await _categoryRepository.SaveAsync(category);

        // Mapear a DTO de respuesta
        return _mapper.Map<CategoryResponseDto>(category);
    }
}
