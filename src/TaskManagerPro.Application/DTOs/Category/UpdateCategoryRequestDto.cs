using TaskManagerPro.Domain.Enums;

namespace TaskManagerPro.Application.DTOs.Category;

/// <summary>
/// DTO para actualizar una categoría
/// </summary>
public class UpdateCategoryRequestDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CategoryType? Type { get; set; }
}
