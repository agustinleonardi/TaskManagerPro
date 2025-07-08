using MediatR;
using TaskManagerPro.Application.DTOs.User;

namespace TaskManagerPro.Application.Queries.User;

/// <summary>
/// Query para obtener todos los usuarios del sistema
/// </summary>
public class GetAllUsersQuery : IRequest<IEnumerable<UserResponseDto>>
{
}
