namespace TaskManagerPro.Application.DTOs.User;

/// <summary>
/// DTO para crear un nuevo usuario
/// </summary>
public class CreateUserRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
