using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Application.DTOs.Task;

/// <summary>
/// DTO para crear una nueva tarea
/// </summary>
public class CreateTaskRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DomainTaskPriority Priority { get; set; }
    public Guid UserId { get; set; }
    public Guid? CategoryId { get; set; }
}
