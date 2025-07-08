using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Repositories;
using TaskManagerPro.Domain.ValueObjects;

namespace TaskManagerPro.Infrastructure.Repositories;

/// <summary>
/// Implementación in-memory del repositorio de usuarios
/// En producción esto sería reemplazado por Entity Framework
/// </summary>
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = _users.FirstOrDefault(u => u.Email.Value == email.Value);
        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        var user = _users.FirstOrDefault(u => u.Username.Value == username.Value);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var exists = _users.Any(u => u.Email.Value == email.Value);
        return Task.FromResult(exists);
    }

    public Task<bool> ExistsByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        var exists = _users.Any(u => u.Username.Value == username.Value);
        return Task.FromResult(exists);
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<User>>(_users.ToList());
    }

    public Task<IEnumerable<User>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        var users = _users.Skip(skip).Take(take).ToList();
        return Task.FromResult<IEnumerable<User>>(users);
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _users.Remove(existingUser);
        }
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }
}
