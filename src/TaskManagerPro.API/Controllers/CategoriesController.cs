using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagerPro.Application.DTOs.Category;
using TaskManagerPro.Application.Queries.Category;

namespace TaskManagerPro.API.Controllers;

/// <summary>
/// Controlador para operaciones de categorías
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener todas las categorías
    /// </summary>
    /// <returns>Lista de categorías</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtener categoría por ID
    /// </summary>
    /// <param name="id">ID de la categoría</param>
    /// <returns>Categoría encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound($"Categoría con ID {id} no encontrada");
            
        return Ok(result);
    }
}
