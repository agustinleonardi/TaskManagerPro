using AutoMapper;
using MediatR;
using TaskManagerPro.Application.DTOs.Task;
using TaskManagerPro.Domain.Repositories;

namespace TaskManagerPro.Application.Queries.Task;

/// <summary>
/// Handler para procesar la consulta de obtener tareas por usuario
/// </summary>
public class GetTasksByUserQueryHandler : IRequestHandler<GetTasksByUserQuery, IEnumerable<TaskResponseDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTasksByUserQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskResponseDto>> Handle(GetTasksByUserQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetByUserIdAsync(request.UserId);
        
        return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
    }
}
