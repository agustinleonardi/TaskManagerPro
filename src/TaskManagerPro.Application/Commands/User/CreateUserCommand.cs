using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.User;

namespace TaskManagerPro.Application.Commands.User;

/// <summary>
/// Comando para crear un nuevo usuario en el sistema
/// </summary>

public class CreateUserCommand : IRequest<UserResponseDto>
{
    public CreateUserRequestDto UserData { get; set; }
    public CreateUserCommand(CreateUserRequestDto userRequestDto)
    {
        UserData = userRequestDto;
    }

    public CreateUserCommand()
    {
        UserData = new CreateUserRequestDto();
    }


}