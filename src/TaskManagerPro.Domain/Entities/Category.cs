using TaskManagerPro.Domain.ValueObjects;
using TaskManagerPro.Domain.Exceptions;
using TaskManagerPro.Domain.Enums;

namespace TaskManagerPro.Domain.Entities;

/// <summary>
/// Entidad que representa una categoría para organizar tareas
/// </summary>
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CategoryType Type { get; private set; }
    public string Color { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Constructor privado para EF Core
    private Category()
    {
        Name = string.Empty;
        Color = string.Empty;
    }

    /// <summary>
    /// Constructor para crear una nueva categoría
    /// </summary>
    public Category(string name, string? description, CategoryType type, Guid userId)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío", nameof(name));

        if (name.Trim().Length > 100)
            throw new ArgumentException("El nombre de la categoría no puede exceder 100 caracteres", nameof(name));

        // Asignación de propiedades
        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description?.Trim();
        Type = type;
        Color = GetColorForType(type);
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío", nameof(newName));

        if (newName.Trim().Length > 100)
            throw new ArgumentException("El nombre de la categoría no puede exceder 100 caracteres", nameof(newName));

        Name = newName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDescription(string? newDescription)
    {
        Description = newDescription?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cambia el tipo de categoría y actualiza el color automáticamente
    /// </summary>
    public void ChangeType(CategoryType newType)
    {
        Type = newType;
        Color = GetColorForType(newType);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Obtiene el color predefinido para un tipo de categoría
    /// </summary>
    private static string GetColorForType(CategoryType type)
    {
        return type switch
        {
            CategoryType.Work => "#2E86AB",      // Azul profesional
            CategoryType.Personal => "#F24236",  // Rojo cálido
            CategoryType.Health => "#4CAF50",    // Verde salud
            CategoryType.Finance => "#FF9800",   // Naranja dinero
            CategoryType.Learning => "#9C27B0",  // Morado conocimiento
            CategoryType.Social => "#00BCD4",    // Cian social
            CategoryType.Home => "#795548",      // Marrón hogar
            CategoryType.Projects => "#E91E63",  // Rosa creativo
            CategoryType.Travel => "#607D8B",    // Azul gris viaje
            CategoryType.Urgent => "#FF5722",    // Rojo naranja urgente
            _ => "#6C757D"                       // Gris por defecto
        };
    }

}
