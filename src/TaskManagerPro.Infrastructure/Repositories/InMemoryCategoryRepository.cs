using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Infrastructure.Repositories;

/// <summary>
/// Implementación in-memory del repositorio de categorías
/// En producción esto sería reemplazado por Entity Framework
/// </summary>
public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new();

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(category);
    }

    public Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var categories = _categories.Where(c => c.UserId == userId).ToList();
        return Task.FromResult<IEnumerable<Category>>(categories);
    }

    public Task<Category?> GetByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default)
    {
        var category = _categories.FirstOrDefault(c => c.Name == name && c.UserId == userId);
        return Task.FromResult(category);
    }

    public Task<bool> ExistsByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default)
    {
        var exists = _categories.Any(c => c.Name == name && c.UserId == userId);
        return Task.FromResult(exists);
    }

    public Task SaveAsync(Category category, CancellationToken cancellationToken = default)
    {
        var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
        if (existingCategory != null)
        {
            _categories.Remove(existingCategory);
        }
        _categories.Add(category);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
    {
        _categories.Remove(category);
        return Task.CompletedTask;
    }

    public Task DeleteAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _categories.RemoveAll(c => c.UserId == userId);
        return Task.CompletedTask;
    }
}
