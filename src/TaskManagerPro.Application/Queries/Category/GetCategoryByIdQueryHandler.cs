using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Category;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Queries.Category;

/// <summary>
/// Handler para procesar la consulta de obtener categoría por ID
/// </summary>
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto?>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        
        return category == null ? null : _mapper.Map<CategoryResponseDto>(category);
    }
}
