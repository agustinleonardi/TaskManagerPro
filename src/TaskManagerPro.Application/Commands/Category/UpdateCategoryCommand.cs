using MediatR;
using TaskManagerPro.Application.DTOs.Category;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Comando para actualizar una categoría existente
/// </summary>
public class UpdateCategoryCommand : IRequest<CategoryResponseDto>
{
    public Guid CategoryId { get; set; }
    public UpdateCategoryRequestDto CategoryData { get; set; }

    public UpdateCategoryCommand(Guid categoryId, UpdateCategoryRequestDto categoryData)
    {
        CategoryId = categoryId;
        CategoryData = categoryData;
    }

    public UpdateCategoryCommand()
    {
        CategoryData = new UpdateCategoryRequestDto();
    }
}
