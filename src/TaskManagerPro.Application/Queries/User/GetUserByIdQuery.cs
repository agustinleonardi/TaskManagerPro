using MediatR;
using TaskManagerPro.Application.DTOs.User;

namespace TaskManagerPro.Application.Queries.User;

/// <summary>
/// Query para obtener un usuario por su ID
/// </summary>
public class GetUserByIdQuery : IRequest<UserResponseDto?>
{
    public Guid UserId { get; set; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }

    public GetUserByIdQuery()
    {
    }
}
