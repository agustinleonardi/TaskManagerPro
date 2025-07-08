using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Queries.User;

/// <summary>
/// Handler para procesar la consulta de obtener usuario por email
/// </summary>
public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        return user == null ? null : _mapper.Map<UserResponseDto>(user);
    }
}
