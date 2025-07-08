using TaskManagerPro.Domain.Enums;

namespace TaskManagerPro.Application.DTOs.Category;

/// <summary>
/// DTO para crear una nueva categoría
/// </summary>
public class CreateCategoryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CategoryType Type { get; set; }
    public Guid UserId { get; set; }
}
