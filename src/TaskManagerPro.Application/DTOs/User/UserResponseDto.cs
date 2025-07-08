namespace TaskManagerPro.Application.DTOs.User;

/// <summary>
/// DTO para la respuesta de usuario
/// </summary>
public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int TaskCount { get; set; }
    public int CompletedTasksCount { get; set; }
}
