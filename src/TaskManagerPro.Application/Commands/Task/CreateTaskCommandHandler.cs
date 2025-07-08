using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Task;
using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Repositories;
using TaskManagerPro.Domain.ValueObjects;

namespace TaskManagerPro.Application.Commands.Task;

/// <summary>
/// Handler para procesar el comando de crear tarea
/// </summary>
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskResponseDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<TaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Verificar que el usuario existe
        var user = await _userRepository.GetByIdAsync(request.TaskData.UserId);
        if (user == null)
            throw new ArgumentException($"Usuario con ID {request.TaskData.UserId} no encontrado");

        // Verificar que la categoría existe (si se especifica)
        if (request.TaskData.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.TaskData.CategoryId.Value);
            if (category == null)
                throw new ArgumentException($"Categoría con ID {request.TaskData.CategoryId} no encontrada");
        }

        // Crear la entidad tarea
        var task = new TaskItem(
            new TaskTitle(request.TaskData.Title),
            new TaskDescription(request.TaskData.Description),
            request.TaskData.Priority,
            request.TaskData.UserId,
            request.TaskData.CategoryId
        );

        // Guardar en repositorio
        await _taskRepository.SaveAsync(task);

        // Mapear a DTO de respuesta
        return _mapper.Map<TaskResponseDto>(task);
    }
}
