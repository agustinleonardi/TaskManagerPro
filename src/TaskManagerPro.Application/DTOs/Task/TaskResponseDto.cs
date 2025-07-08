using DomainTaskStatus = TaskManagerPro.Domain.Enums.TaskStatus;
using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Application.DTOs.Task;

/// <summary>
/// DTO para la respuesta de tarea
/// </summary>
public class TaskResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DomainTaskStatus Status { get; set; }
    public DomainTaskPriority Priority { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public string PriorityName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsInProgress { get; set; }
    public bool IsPending { get; set; }
}
