using MediatR;
using TaskManagerPro.Application.DTOs.User;

namespace TaskManagerPro.Application.Queries.User;

/// <summary>
/// Query para obtener un usuario por su email
/// </summary>
public class GetUserByEmailQuery : IRequest<UserResponseDto?>
{
    public string Email { get; set; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }

    public GetUserByEmailQuery()
    {
        Email = string.Empty;
    }
}
