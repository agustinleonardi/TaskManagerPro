namespace TaskManagerPro.Application.DTOs.User;

/// <summary>
/// DTO para actualizar un usuario
/// </summary>
public class UpdateUserRequestDto
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
