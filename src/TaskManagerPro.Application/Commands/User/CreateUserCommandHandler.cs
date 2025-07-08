using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Application.Services; // ✅ Application conoce su propia interfaz
using TaskManagerPro.Domain.Repositories;
using TaskManagerPro.Domain.ValueObjects;
using DomainUser = TaskManagerPro.Domain.Entities.User;

namespace TaskManagerPro.Application.Commands.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // ✅ Hash seguro usando BCrypt
        var passwordHash = _passwordHasher.HashPassword(request.UserData.Password);

        // Crear Value Objects
        var email = new Email(request.UserData.Email);
        var username = new Username(request.UserData.Username);

        // Verificar que email no esté registrado
        var existingUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"El email {email.Value} ya está registrado");
        }

        // Crear entidad de dominio
        var user = new DomainUser(email, username, passwordHash);

        // Persistir en la base de datos
        await _userRepository.SaveAsync(user, cancellationToken);

        // Mapear entidad a DTO de respuesta
        return _mapper.Map<UserResponseDto>(user);
    }
}