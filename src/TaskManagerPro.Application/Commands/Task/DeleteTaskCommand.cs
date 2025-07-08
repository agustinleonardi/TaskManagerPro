using MediatR;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Comando para eliminar una tarea
/// </summary>
public class DeleteTaskCommand : IRequest<bool>
{
    public Guid TaskId { get; set; }

    public DeleteTaskCommand(Guid taskId)
    {
        TaskId = taskId;
    }

    public DeleteTaskCommand()
    {
    }
}
