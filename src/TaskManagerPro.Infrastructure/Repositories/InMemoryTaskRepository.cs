using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Infrastructure.Repositories;

/// <summary>
/// Implementación in-memory del repositorio de tareas
/// En producción esto sería reemplazado por Entity Framework
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tasks = _tasks.Where(t => t.UserId == userId).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<IEnumerable<TaskItem>> GetByUserIdAndStatusAsync(Guid userId, Domain.Enums.TaskStatus status, CancellationToken cancellationToken = default)
    {
        var tasks = _tasks.Where(t => t.UserId == userId && t.Status == status).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<IEnumerable<TaskItem>> GetByUserIdAndPriorityAsync(Guid userId, Domain.Enums.TaskPriority priority, CancellationToken cancellationToken = default)
    {
        var tasks = _tasks.Where(t => t.UserId == userId && t.Priority == priority).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<IEnumerable<TaskItem>> GetByUserIdAndCategoryAsync(Guid userId, Guid? categoryId, CancellationToken cancellationToken = default)
    {
        var tasks = _tasks.Where(t => t.UserId == userId && t.CategoryId == categoryId).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<IEnumerable<TaskItem>> GetCompletedTasksByDateRangeAsync(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        var tasks = _tasks.Where(t => t.UserId == userId && t.IsCompleted && 
                                    t.CompletedAt.HasValue &&
                                    t.CompletedAt.Value >= from && 
                                    t.CompletedAt.Value <= to).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<IEnumerable<TaskItem>> GetPendingTasksDueSoonAsync(Guid userId, DateTime dueDate, CancellationToken cancellationToken = default)
    {
        // Para esta implementación básica, no hay fecha de vencimiento en TaskItem
        var tasks = _tasks.Where(t => t.UserId == userId && t.IsPending).ToList();
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }

    public Task<Dictionary<Domain.Enums.TaskStatus, int>> GetTaskCountByStatusAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var counts = _tasks.Where(t => t.UserId == userId)
                          .GroupBy(t => t.Status)
                          .ToDictionary(g => g.Key, g => g.Count());
        return Task.FromResult(counts);
    }

    public Task SaveAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            _tasks.Remove(existingTask);
        }
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        _tasks.Remove(task);
        return Task.CompletedTask;
    }

    public Task DeleteAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _tasks.RemoveAll(t => t.UserId == userId);
        return Task.CompletedTask;
    }
}
