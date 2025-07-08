using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Application.DTOs.Task;

/// <summary>
/// DTO para actualizar una tarea
/// </summary>
public class UpdateTaskRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DomainTaskPriority? Priority { get; set; }
    public Guid? CategoryId { get; set; }
}
