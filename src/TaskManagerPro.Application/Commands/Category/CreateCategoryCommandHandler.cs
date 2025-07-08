using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Category;
using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Handler para el comando de creación de categoría
/// </summary>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponseDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad Category desde el DTO
        var category = new TaskManagerPro.Domain.Entities.Category(
            request.CategoryData.Name,
            request.CategoryData.Description,
            request.CategoryData.Type,
            request.CategoryData.UserId
        );

        // 2. Guardar en el repositorio
        await _categoryRepository.SaveAsync(category, cancellationToken);

        // 3. Mapear a DTO de respuesta y retornar
        return _mapper.Map<CategoryResponseDto>(category);
    }
}
