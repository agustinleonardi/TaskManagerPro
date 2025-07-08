using MediatR;

namespace TaskManagerPro.Application.Commands.User;

/// <summary>
/// Comando para eliminar un usuario
/// </summary>
public class DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }

    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }

    public DeleteUserCommand()
    {
    }
}
