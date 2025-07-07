namespace TaskManagerPro.Domain.Enums;

/// <summary>
/// Niveles de prioridad para las tareas
/// </summary>
public enum TaskPriority
{
    /// <summary>
    /// Prioridad baja - no urgente
    /// </summary>
    Low = 0,

    /// <summary>
    /// Prioridad media - normal
    /// </summary>
    Medium = 1,

    /// <summary>
    /// Prioridad alta - importante
    /// </summary>
    High = 2,

    /// <summary>
    /// Prioridad crítica - urgente
    /// </summary>
    Critical = 3
}
