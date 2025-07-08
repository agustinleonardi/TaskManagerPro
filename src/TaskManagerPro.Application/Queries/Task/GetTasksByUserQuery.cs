using MediatR;
using TaskManagerPro.Application.DTOs.Task;

namespace TaskManagerPro.Application.Queries.Task;

/// <summary>
/// Query para obtener todas las tareas de un usuario
/// </summary>
public class GetTasksByUserQuery : IRequest<IEnumerable<TaskResponseDto>>
{
    public Guid UserId { get; set; }

    public GetTasksByUserQuery(Guid userId)
    {
        UserId = userId;
    }

    public GetTasksByUserQuery()
    {
    }
}
