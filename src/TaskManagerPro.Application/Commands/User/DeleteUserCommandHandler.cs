using MediatR;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Commands.User;

/// <summary>
/// Handler para procesar el comando de eliminar usuario
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Verificar que el usuario existe
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new ArgumentException($"Usuario con ID {request.UserId} no encontrado");

        // Eliminar el usuario
        await _userRepository.DeleteAsync(user);

        return true;
    }
}
