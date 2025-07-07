using TaskManagerPro.Domain.Entities;
using DomainTaskStatus = TaskManagerPro.Domain.Enums.TaskStatus;
using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Domain.Repositories;

/// <summary>
/// Contrato para persistencia de tareas
/// Define las operaciones necesarias sin especificar la implementación
/// </summary>
public interface ITaskRepository
{
    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las tareas de un usuario
    /// </summary>
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene tareas de un usuario filtradas por estado
    /// </summary>
    Task<IEnumerable<TaskItem>> GetByUserIdAndStatusAsync(Guid userId, DomainTaskStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene tareas de un usuario filtradas por prioridad
    /// </summary>
    Task<IEnumerable<TaskItem>> GetByUserIdAndPriorityAsync(Guid userId, DomainTaskPriority priority, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene tareas de un usuario por categoría
    /// </summary>
    Task<IEnumerable<TaskItem>> GetByUserIdAndCategoryAsync(Guid userId, Guid? categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene tareas completadas de un usuario en un rango de fechas
    /// </summary>
    Task<IEnumerable<TaskItem>> GetCompletedTasksByDateRangeAsync(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene tareas pendientes que vencen pronto
    /// </summary>
    Task<IEnumerable<TaskItem>> GetPendingTasksDueSoonAsync(Guid userId, DateTime dueDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cuenta las tareas por estado para un usuario
    /// </summary>
    Task<Dictionary<DomainTaskStatus, int>> GetTaskCountByStatusAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Guarda una tarea (nueva o actualizada)
    /// </summary>
    Task SaveAsync(TaskItem task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina todas las tareas de un usuario
    /// </summary>
    Task DeleteAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
