using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Queries.User;

/// <summary>
/// Handler para procesar la consulta de obtener usuario por ID
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        return user == null ? null : _mapper.Map<UserResponseDto>(user);
    }
}
