using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.ValueObjects;

namespace TaskManagerPro.Domain.Repositories;

/// <summary>
/// Contrato para persistencia de usuarios
/// Define las operaciones necesarias sin especificar la implementación
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un usuario por su ID
    /// </summary>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un usuario por su username
    /// </summary>
    Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un usuario con el email especificado
    /// </summary>
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un usuario con el username especificado
    /// </summary>
    Task<bool> ExistsByUsernameAsync(Username username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los usuarios (para administración)
    /// </summary>
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene usuarios con paginación
    /// </summary>
    Task<IEnumerable<User>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken = default);

    /// <summary>
    /// Guarda un usuario (nuevo o actualizado)
    /// </summary>
    Task SaveAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}
