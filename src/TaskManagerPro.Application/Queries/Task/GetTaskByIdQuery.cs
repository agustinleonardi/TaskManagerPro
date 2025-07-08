using MediatR;
using TaskManagerPro.Application.DTOs.Task;

namespace TaskManagerPro.Application.Queries.Task;

/// <summary>
/// Query para obtener una tarea por su ID
/// </summary>
public class GetTaskByIdQuery : IRequest<TaskResponseDto?>
{
    public Guid TaskId { get; set; }

    public GetTaskByIdQuery(Guid taskId)
    {
        TaskId = taskId;
    }

    public GetTaskByIdQuery()
    {
    }
}
