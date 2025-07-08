using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Application.Services;
using TaskManagerPro.Domain.Repositories;
using TaskManagerPro.Domain.ValueObjects;

namespace TaskManagerPro.Application.Commands.User;

/// <summary>
/// Handler para procesar el comando de actualizar usuario
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Obtener el usuario existente
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new ArgumentException($"Usuario con ID {request.UserId} no encontrado");

        // Actualizar propiedades
        if (!string.IsNullOrWhiteSpace(request.UserData.Username))
        {
            var newUsername = new Username(request.UserData.Username);
            user.ChangeUsername(newUsername);
        }

        if (!string.IsNullOrWhiteSpace(request.UserData.Email))
        {
            var newEmail = new Email(request.UserData.Email);
            user.ChangeEmail(newEmail);
        }

        if (!string.IsNullOrWhiteSpace(request.UserData.Password))
        {
            var hashedPassword = _passwordHasher.HashPassword(request.UserData.Password);
            user.ChangePasswordHash(hashedPassword);
        }

        // Guardar cambios
        await _userRepository.SaveAsync(user);

        // Mapear a DTO de respuesta
        return _mapper.Map<UserResponseDto>(user);
    }
}
