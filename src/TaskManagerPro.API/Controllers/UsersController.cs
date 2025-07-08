using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagerPro.Application.Commands.User;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Application.Queries.User;

namespace TaskManagerPro.API.Controllers;

/// <summary>
/// Controlador para operaciones de usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crear un nuevo usuario
    /// </summary>
    /// <param name="request">Datos del usuario a crear</param>
    /// <returns>Usuario creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserRequestDto request)
    {
        var command = new CreateUserCommand(request);
        var result = await _mediator.Send(command);
        return Created($"api/users/{result.Id}", result);
    }

    /// <summary>
    /// Obtener un usuario por su ID
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <returns>Usuario encontrado</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetUser(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound($"Usuario con ID {id} no encontrado");

        return Ok(result);
    }

    /// <summary>
    /// Obtener todos los usuarios
    /// </summary>
    /// <returns>Lista de todos los usuarios</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtener un usuario por su email
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>Usuario encontrado</returns>
    [HttpGet("by-email/{email}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetUserByEmail(string email)
    {
        var query = new GetUserByEmailQuery(email);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound($"Usuario con email {email} no encontrado");

        return Ok(result);
    }
}
