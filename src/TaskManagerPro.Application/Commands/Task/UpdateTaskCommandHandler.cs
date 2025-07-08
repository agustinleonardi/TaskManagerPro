using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Task;
using TaskManagerPro.Domain.Repositories;
using TaskManagerPro.Domain.ValueObjects;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Handler para procesar el comando de actualizar tarea
/// </summary>
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskResponseDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<TaskResponseDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Obtener la tarea existente
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null)
            throw new ArgumentException($"Tarea con ID {request.TaskId} no encontrada");

        // Verificar que la categoría existe (si se especifica)
        if (request.TaskData.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.TaskData.CategoryId.Value);
            if (category == null)
                throw new ArgumentException($"Categoría con ID {request.TaskData.CategoryId} no encontrada");
        }

        // Actualizar propiedades
        if (!string.IsNullOrWhiteSpace(request.TaskData.Title))
            task.ChangeTitle(new TaskTitle(request.TaskData.Title));

        if (!string.IsNullOrWhiteSpace(request.TaskData.Description))
            task.ChangeDescription(new TaskDescription(request.TaskData.Description));

        if (request.TaskData.Priority.HasValue)
            task.ChangePriority(request.TaskData.Priority.Value);

        if (request.TaskData.CategoryId.HasValue)
            task.AssignToCategory(request.TaskData.CategoryId);

        // Guardar cambios
        await _taskRepository.SaveAsync(task);

        // Mapear a DTO de respuesta
        return _mapper.Map<TaskResponseDto>(task);
    }
}
