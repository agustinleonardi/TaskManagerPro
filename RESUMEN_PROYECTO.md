# 📋 TaskManagerPro V2 - Resumen Completo del Proyecto

## 🎯 Objetivo del Proyecto
Crear una API REST para gestión de tareas usando **Clean Architecture**, **CQRS**, **MediatR** y buenas prácticas de desarrollo, construyendo desde cero para entender cada decisión técnica.

---

## 🏗️ Estructura del Proyecto Creada

```
TaskManagerPro_V2/
├── TaskManagerPro.sln                 # Archivo de solución principal
├── src/                               # Código fuente
│   ├── TaskManagerPro.Domain/         # 🟢 Núcleo del negocio
│   ├── TaskManagerPro.Application/    # 🔵 Casos de uso y lógica aplicación
│   ├── TaskManagerPro.Infrastructure/ # 🟡 Acceso a datos y servicios
│   └── TaskManagerPro.API/           # 🔴 Controladores web y endpoints
└── tests/                            # Tests unitarios
    └── TaskManagerPro.Tests/
```

---

## 🔄 Dependencias entre Proyectos (Clean Architecture)

```
┌─────────────┐
│   Domain    │ ← Independiente de todo (Entidades, Reglas de Negocio)
└─────────────┘
       ↑
┌─────────────┐
│ Application │ ← Solo depende de Domain (Casos de uso, Interfaces)
└─────────────┘
       ↑
┌─────────────┐
│Infrastructure│ ← Depende de Domain + Application (Implementaciones)
└─────────────┘
       ↑
┌─────────────┐
│     API     │ ← Depende de Application + Infrastructure (Controllers)
└─────────────┘
```

### ✅ **¿Por qué esta estructura?**
- **Domain**: Núcleo puro del negocio, sin dependencias externas
- **Application**: Orquesta el dominio, define contratos (interfaces)
- **Infrastructure**: Implementa los detalles técnicos (BD, servicios)
- **API**: Capa de presentación, solo maneja HTTP

---

## 🛠️ Comandos Ejecutados para Crear el Proyecto

### 1. Estructura base
```bash
cd /Users/agustinleonardi/Desktop/CSharp
mkdir TaskManagerPro_V2 && cd TaskManagerPro_V2
mkdir -p src tests && cd src
```

### 2. Creación de proyectos
```bash
# Domain - Núcleo del negocio
dotnet new classlib -n TaskManagerPro.Domain

# Application - Casos de uso
dotnet new classlib -n TaskManagerPro.Application

# Infrastructure - Acceso a datos
dotnet new classlib -n TaskManagerPro.Infrastructure

# API - Controladores web
dotnet new webapi -n TaskManagerPro.API

# Tests - Pruebas unitarias
cd ../tests
dotnet new xunit -n TaskManagerPro.Tests
```

### 3. Archivo de solución
```bash
cd ..
dotnet new sln -n TaskManagerPro

# Agregar todos los proyectos
dotnet sln add src/TaskManagerPro.Domain/TaskManagerPro.Domain.csproj
dotnet sln add src/TaskManagerPro.Application/TaskManagerPro.Application.csproj
dotnet sln add src/TaskManagerPro.Infrastructure/TaskManagerPro.Infrastructure.csproj
dotnet sln add src/TaskManagerPro.API/TaskManagerPro.API.csproj
dotnet sln add tests/TaskManagerPro.Tests/TaskManagerPro.Tests.csproj
```

### 4. Referencias entre proyectos
```bash
# Application → Domain
dotnet add src/TaskManagerPro.Application/TaskManagerPro.Application.csproj reference src/TaskManagerPro.Domain/TaskManagerPro.Domain.csproj

# Infrastructure → Domain + Application
dotnet add src/TaskManagerPro.Infrastructure/TaskManagerPro.Infrastructure.csproj reference src/TaskManagerPro.Domain/TaskManagerPro.Domain.csproj
dotnet add src/TaskManagerPro.Infrastructure/TaskManagerPro.Infrastructure.csproj reference src/TaskManagerPro.Application/TaskManagerPro.Application.csproj

# API → Application + Infrastructure
dotnet add src/TaskManagerPro.API/TaskManagerPro.API.csproj reference src/TaskManagerPro.Application/TaskManagerPro.Application.csproj
dotnet add src/TaskManagerPro.API/TaskManagerPro.API.csproj reference src/TaskManagerPro.Infrastructure/TaskManagerPro.Infrastructure.csproj
```

---

## 🧠 Conceptos Clave Explicados

### 🎯 **Clean Architecture**
**¿Qué es?** Arquitectura que separa responsabilidades en capas concéntricas.

**¿Por qué usarla?**
- ✅ **Independencia de frameworks**: Lógica no depende de ASP.NET, EF, etc.
- ✅ **Testeable**: Puedes testear sin BD, sin HTTP
- ✅ **Independencia de BD**: Cambiar PostgreSQL por SQL Server sin tocar lógica
- ✅ **Separación clara**: Cada capa tiene un propósito específico

### 🎯 **Reglas de Negocio - ¿Dónde van?**

| Tipo de Regla | Ubicación | Ejemplo |
|---------------|-----------|---------|
| **Validación de formato** | Domain (Value Objects) | Email válido, Username formato |
| **Invariantes de entidad** | Domain (Entidades) | Máximo 50 tareas por usuario |
| **Reglas de negocio puras** | Domain (Métodos) | Solo se puede eliminar tarea no completada |
| **Validación de casos de uso** | Application (Handlers) | Usuario debe existir para crear tarea |
| **Reglas de plan/subscripción** | Application (Handlers) | Plan gratuito = máximo 10 tareas |
| **Validaciones de entrada** | Application (Validators) | Campos requeridos, longitudes |

**Ejemplo práctico:**
```csharp
// Domain - Regla invariante
public class User
{
    public void AddTask(TaskItem task)
    {
        if (Tasks.Count >= 50)
            throw new DomainException("Un usuario no puede tener más de 50 tareas");
        Tasks.Add(task);
    }
}

// Application - Regla de caso de uso
public class CreateTaskHandler
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException("Usuario no encontrado");
            
        // La entidad valida sus propias reglas
        user.AddTask(new TaskItem(request.Title, request.Description));
    }
}
```

---

## 🎯 **Próximos Pasos Planificados**

### **Fase 1: Domain First (Fundamentos)**
1. **Crear entidades**: `User`, `TaskItem`, `Role`
2. **Value Objects**: `Email`, `Username`
3. **Excepciones de dominio**: `DomainException`
4. **Interfaces de repositorio**: `IUserRepository`, `ITaskRepository`

### **Fase 2: Application (Casos de Uso)**
5. **MediatR + CQRS**: Commands y Queries
6. **FluentValidation**: Validaciones robustas
7. **DTOs y Mappers**: AutoMapper o mapeo manual

### **Fase 3: Infrastructure (Detalles Técnicos)**
8. **Entity Framework Core**: DbContext y configuraciones
9. **PostgreSQL**: Conexión y migraciones
10. **Repositorios**: Implementación de interfaces

### **Fase 4: API (Presentación)**
11. **Controllers**: Endpoints REST
12. **JWT Authentication**: Seguridad
13. **Swagger**: Documentación automática

### **Fase 5: Testing y Calidad**
14. **Unit Tests**: xUnit + Moq + FluentAssertions
15. **Integration Tests**: TestContainers para PostgreSQL
16. **Performance**: Logging y métricas

---

## 🛠️ Stack Tecnológico Planificado

### **Backend**
- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - API REST
- **C#** - Lenguaje de programación

### **Arquitectura y Patrones**
- **Clean Architecture** - Separación en capas
- **CQRS** - Commands y Queries separados
- **MediatR** - Patrón Mediator
- **Repository Pattern** - Abstracción de datos
- **Dependency Injection** - IoC nativo de .NET

### **Base de Datos**
- **PostgreSQL** - Base de datos principal
- **Entity Framework Core** - ORM
- **Code-First Migrations** - Versionado de BD

### **Seguridad**
- **JWT** - Autenticación
- **BCrypt** - Hash de contraseñas
- **Claims-based auth** - Autorización

### **Validación y Documentación**
- **FluentValidation** - Validaciones expresivas
- **Swagger/OpenAPI** - Documentación automática

### **Testing**
- **xUnit** - Framework de tests
- **Moq** - Mocking library
- **FluentAssertions** - Assertions legibles
- **TestContainers** - Tests de integración

---

## 🔍 **¿Qué hemos aprendido hasta ahora?**

### **Conceptos Arquitectónicos**
✅ **Clean Architecture** y sus beneficios
✅ **Separación de responsabilidades** por capas
✅ **Reglas de dependencias** (hacia adentro)
✅ **Dónde ubicar reglas de negocio**

### **Herramientas .NET**
✅ **dotnet CLI** para crear proyectos
✅ **Solution files** para agrupar proyectos
✅ **Project references** para dependencias

### **Buenas Prácticas**
✅ **Estructura de carpetas** organizada
✅ **Naming conventions** consistentes
✅ **Separación src/tests**

---

## 🚀 **Para Continuar**

1. **Abrir el proyecto en VS Code:**
   ```bash
   cd /Users/agustinleonardi/Desktop/CSharp/TaskManagerPro_V2
   code .
   ```

2. **Empezar por Domain:**
   - Crear entidades `User` y `TaskItem`
   - Definir reglas de negocio
   - Crear interfaces de repositorio

3. **Continuar con Application:**
   - Instalar MediatR
   - Crear Commands y Queries
   - Implementar validaciones

---

## 💡 **Recordatorios Importantes**

- **Siempre empezar por Domain** (independiente)
- **Entender el "por qué"** de cada patrón
- **Testear cada capa** independientemente
- **Mantener las reglas de dependencias**
- **Documentar decisiones técnicas**

---

*Creado el 4 de julio de 2025 - TaskManagerPro V2 con Clean Architecture*
