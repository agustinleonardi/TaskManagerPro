using MediatR;
using TaskManagerPro.Application.DTOs.Task;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Comando para actualizar una tarea existente
/// </summary>
public class UpdateTaskCommand : IRequest<TaskResponseDto>
{
    public Guid TaskId { get; set; }
    public UpdateTaskRequestDto TaskData { get; set; }

    public UpdateTaskCommand(Guid taskId, UpdateTaskRequestDto taskData)
    {
        TaskId = taskId;
        TaskData = taskData;
    }

    public UpdateTaskCommand()
    {
        TaskData = new UpdateTaskRequestDto();
    }
}
