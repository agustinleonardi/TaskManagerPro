using TaskManagerPro.Domain.ValueObjects;
using TaskManagerPro.Domain.Exceptions;

namespace TaskManagerPro.Domain.Entities;

/// <summary>
/// Entidad raíz de agregado que representa un usuario del sistema
/// </summary>
public class User
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    private readonly List<TaskItem> _tasks = new();
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    // Constructor privado para EF Core
    private User()
    {
        PasswordHash = string.Empty;
    }

    /// <summary>
    /// Constructor para crear un nuevo usuario
    /// </summary>
    public User(Email email, Username username, string passwordHash)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException(nameof(passwordHash), "El hash de contraseña no puede estar vacío");

        Id = Guid.NewGuid();
        Email = email;
        Username = username;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTask(TaskItem task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (_tasks.Count >= 50)
            throw new UserDomainException("Un usuario no puede tener mas de 50 tareas");

        if (_tasks.Any(t => t.Title.Value.Equals(task.Title.Value, StringComparison.OrdinalIgnoreCase)))
            throw new UserDomainException("Ya existe una tarea con ese titulo");

        _tasks.Add(task);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Elimina una tarea del usuario
    /// </summary>
    public void RemoveTask(TaskItem task)
    {
        if (!_tasks.Contains(task))
            throw new UserDomainException("La tarea no pertenece a este usuario");

        if (task.IsCompleted)
            throw new UserDomainException("No se puede eliminar una tarea completada");

        _tasks.Remove(task);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia el email del usuario
    /// </summary>
    public void ChangeEmail(Email newEmail)
    {
        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia el username del usuario
    /// </summary>
    public void ChangeUsername(Username newUsername)
    {
        Username = newUsername;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia el hash de contraseña del usuario
    /// </summary>
    public void ChangePasswordHash(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("El hash de contraseña no puede estar vacío", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Obtiene el número de tareas completadas
    /// </summary>
    public int GetCompletedTasksCount()
    {
        return _tasks.Count(t => t.IsCompleted);
    }

    /// <summary>
    /// Obtiene el número de tareas pendientes
    /// </summary>
    public int GetPendingTasksCount()
    {
        return _tasks.Count(t => t.IsPending);
    }

    /// <summary>
    /// Verifica si el usuario puede agregar más tareas
    /// </summary>
    public bool CanAddMoreTasks()
    {
        return _tasks.Count < 50;
    }
}
