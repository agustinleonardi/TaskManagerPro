using TaskManagerPro.Domain.ValueObjects;
using TaskManagerPro.Domain.Exceptions;
using DomainTaskStatus = TaskManagerPro.Domain.Enums.TaskStatus;
using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Domain.Entities;

/// <summary>
/// Entidad que representa una tarea individual
/// </summary>
public class TaskItem
{
    public Guid Id { get; private set; }
    public TaskTitle Title { get; private set; }
    public TaskDescription Description { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DomainTaskStatus Status { get; private set; }
    public DomainTaskPriority Priority { get; private set; }

    // 🔒 PROPIEDADES CALCULADAS
    public bool IsCompleted => Status == DomainTaskStatus.Completed;
    public bool IsCancelled => Status == DomainTaskStatus.Cancelled;
    public bool IsInProgress => Status == DomainTaskStatus.InProgress;
    public bool IsPending => Status == DomainTaskStatus.Pending;

    // 🏗️ CONSTRUCTORES

    // Constructor privado para EF Core
    private TaskItem() { }

    /// <summary>
    /// Constructor para crear una nueva tarea
    /// </summary>
    public TaskItem(TaskTitle title, TaskDescription description, DomainTaskPriority priority, Guid userId, Guid? categoryId = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        UserId = userId;
        CategoryId = categoryId;
        Status = DomainTaskStatus.Pending; // Siempre empieza como Pending
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // 🎯 MÉTODOS DE NEGOCIO

    /// <summary>
    /// Completa la tarea
    /// </summary>
    public void Complete()
    {
        if (IsCompleted)
            throw new TaskDomainException("La tarea ya está completada");

        if (IsCancelled)
            throw new TaskDomainException("No se puede completar una tarea cancelada");

        Status = DomainTaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancela la tarea
    /// </summary>
    public void Cancel()
    {
        if (IsCompleted)
            throw new TaskDomainException("No se puede cancelar una tarea completada");

        Status = DomainTaskStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Inicia el progreso de la tarea
    /// </summary>
    public void StartProgress()
    {
        if (Status != DomainTaskStatus.Pending)
            throw new TaskDomainException("Solo se puede iniciar el progreso de tareas pendientes");

        Status = DomainTaskStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia el título de la tarea
    /// </summary>
    public void ChangeTitle(TaskTitle newTitle)
    {
        Title = newTitle;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia la descripción de la tarea
    /// </summary>
    public void ChangeDescription(TaskDescription newDescription)
    {
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia la prioridad de la tarea
    /// </summary>
    public void ChangePriority(DomainTaskPriority newPriority)
    {
        Priority = newPriority;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Asigna o cambia la categoría de la tarea
    /// </summary>
    public void AssignToCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }
}