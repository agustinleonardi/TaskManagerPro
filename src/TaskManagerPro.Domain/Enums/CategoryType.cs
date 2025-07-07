namespace TaskManagerPro.Domain.Enums;

/// <summary>
/// Tipos de categoría predefinidos con colores automáticos asignados
/// </summary>
public enum CategoryType
{
    /// <summary>
    /// Tareas relacionadas con el trabajo - Color: #2E86AB (Azul profesional)
    /// </summary>
    Work = 1,

    /// <summary>
    /// Tareas personales y familiares - Color: #F24236 (Rojo cálido)
    /// </summary>
    Personal = 2,

    /// <summary>
    /// Tareas de salud y bienestar - Color: #4CAF50 (Verde salud)
    /// </summary>
    Health = 3,

    /// <summary>
    /// Tareas financieras y económicas - Color: #FF9800 (Naranja dinero)
    /// </summary>
    Finance = 4,

    /// <summary>
    /// Tareas de aprendizaje y educación - Color: #9C27B0 (Morado conocimiento)
    /// </summary>
    Learning = 5,

    /// <summary>
    /// Tareas sociales y eventos - Color: #00BCD4 (Cian social)
    /// </summary>
    Social = 6,

    /// <summary>
    /// Tareas del hogar y mantenimiento - Color: #795548 (Marrón hogar)
    /// </summary>
    Home = 7,

    /// <summary>
    /// Tareas de proyectos y hobbies - Color: #E91E63 (Rosa creativo)
    /// </summary>
    Projects = 8,

    /// <summary>
    /// Tareas de viajes y vacaciones - Color: #607D8B (Azul gris viaje)
    /// </summary>
    Travel = 9,

    /// <summary>
    /// Tareas urgentes e importantes - Color: #FF5722 (Rojo naranja urgente)
    /// </summary>
    Urgent = 10
}
