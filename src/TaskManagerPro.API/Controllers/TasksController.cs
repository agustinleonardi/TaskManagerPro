using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManagerPro.Application.Commands.Task;
using TaskManagerPro.Application.Queries.Task;
using TaskManagerPro.Application.DTOs.Task;

namespace TaskManagerPro.API.Controllers;

/// <summary>
/// Controlador para operaciones CRUD de tareas
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crear una nueva tarea
    /// </summary>
    /// <param name="request">Datos de la tarea a crear</param>
    /// <returns>Tarea creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TaskResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskRequestDto request)
    {
        try
        {
            var command = new CreateTaskCommand(request);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtener una tarea por ID
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <returns>Tarea encontrada</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskResponseDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TaskResponseDto>> GetTaskById(Guid id)
    {
        try
        {
            var query = new GetTaskByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtener todas las tareas de un usuario
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <returns>Lista de tareas del usuario</returns>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<TaskResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasksByUser(Guid userId)
    {
        try
        {
            var query = new GetTasksByUserQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Actualizar una tarea existente
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <param name="request">Datos actualizados de la tarea</param>
    /// <returns>Tarea actualizada</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TaskResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TaskResponseDto>> UpdateTask(Guid id, [FromBody] UpdateTaskRequestDto request)
    {
        try
        {
            var command = new UpdateTaskCommand(id, request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Eliminar una tarea
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <returns>Confirmación de eliminación</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> DeleteTask(Guid id)
    {
        try
        {
            var command = new DeleteTaskCommand(id);
            var result = await _mediator.Send(command);
            return Ok(new { message = "Tarea eliminada exitosamente" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }
}
