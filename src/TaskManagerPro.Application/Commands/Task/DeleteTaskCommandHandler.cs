using MediatR;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Handler para procesar el comando de eliminar tarea
/// </summary>
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        // Verificar que la tarea existe
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null)
            throw new ArgumentException($"Tarea con ID {request.TaskId} no encontrada");

        // Eliminar la tarea
        await _taskRepository.DeleteAsync(task);

        return true;
    }
}
