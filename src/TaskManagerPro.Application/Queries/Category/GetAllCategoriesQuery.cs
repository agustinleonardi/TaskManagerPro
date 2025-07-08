using MediatR;
using TaskManagerPro.Application.DTOs.Category;

namespace TaskManagerPro.Application.Queries.Category;

/// <summary>
/// Query para obtener todas las categorías
/// </summary>
public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponseDto>>
{
}
