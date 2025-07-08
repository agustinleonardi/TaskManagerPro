using TaskManagerPro.Domain.Enums;

namespace TaskManagerPro.Application.DTOs.Category;

/// <summary>
/// DTO para la respuesta de categoría
/// </summary>
public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CategoryType Type { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
