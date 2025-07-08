using MediatR;
using TaskManagerPro.Application.DTOs.User;

namespace TaskManagerPro.Application.Commands.User;

/// <summary>
/// Comando para actualizar un usuario existente
/// </summary>
public class UpdateUserCommand : IRequest<UserResponseDto>
{
    public Guid UserId { get; set; }
    public UpdateUserRequestDto UserData { get; set; }

    public UpdateUserCommand(Guid userId, UpdateUserRequestDto userData)
    {
        UserId = userId;
        UserData = userData;
    }

    public UpdateUserCommand()
    {
        UserData = new UpdateUserRequestDto();
    }
}
