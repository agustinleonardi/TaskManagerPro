using MediatR;
using TaskManagerPro.Application.DTOs.Category;

namespace TaskManagerPro.Application.Commands.Category;

/// <summary>
/// Comando para crear una nueva categoría en el sistema
/// </summary>
public class CreateCategoryCommand : IRequest<CategoryResponseDto>
{
    public CreateCategoryRequestDto CategoryData { get; set; }

    public CreateCategoryCommand(CreateCategoryRequestDto categoryData)
    {
        CategoryData = categoryData;
    }

    public CreateCategoryCommand()
    {
        CategoryData = new CreateCategoryRequestDto();
    }
}
