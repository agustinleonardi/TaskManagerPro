using TaskManagerPro.Domain.Entities;

namespace TaskManagerPro.Domain.Repositories;

/// <summary>
/// Contrato para persistencia de categorías
/// Define las operaciones necesarias sin especificar la implementación
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Obtiene una categoría por su ID
    /// </summary>
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las categorías de un usuario
    /// </summary>
    Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una categoría por nombre y usuario
    /// </summary>
    Task<Category?> GetByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe una categoría con el nombre especificado para un usuario
    /// </summary>
    Task<bool> ExistsByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Guarda una categoría (nueva o actualizada)
    /// </summary>
    Task SaveAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina una categoría
    /// </summary>
    Task DeleteAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina todas las categorías de un usuario
    /// </summary>
    Task DeleteAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
