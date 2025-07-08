using MediatR;
using TaskManagerPro.Application.DTOs.Category;

namespace TaskManagerPro.Application.Queries.Category;

/// <summary>
/// Query para obtener una categoría por su ID
/// </summary>
public class GetCategoryByIdQuery : IRequest<CategoryResponseDto?>
{
    public Guid CategoryId { get; set; }

    public GetCategoryByIdQuery(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public GetCategoryByIdQuery()
    {
    }
}
