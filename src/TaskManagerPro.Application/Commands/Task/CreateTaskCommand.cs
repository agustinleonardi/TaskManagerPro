using MediatR;
using TaskManagerPro.Application.DTOs.Task;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Comando para crear una nueva tarea en el sistema
/// </summary>
public class CreateTaskCommand : IRequest<TaskResponseDto>
{
    public CreateTaskRequestDto TaskData { get; set; }

    public CreateTaskCommand(CreateTaskRequestDto taskData)
    {
        TaskData = taskData;
    }

    public CreateTaskCommand()
    {
        TaskData = new CreateTaskRequestDto();
    }
}
