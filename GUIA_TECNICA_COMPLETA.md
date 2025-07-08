# 📚 Guía Técnica Completa - TaskManagerPro

> **Objetivo:** Documento completo para entender todas las tecnologías, patrones y flujos del proyecto TaskManagerPro.

---

## 📋 **Índice**
1. [Arquitectura General](#arquitectura-general)
2. [Tecnologías Utilizadas](#tecnologías-utilizadas)
3. [Patrones de Diseño](#patrones-de-diseño)
4. [Flujo de Datos Completo](#flujo-de-datos-completo)
5. [Estructura de Capas](#estructura-de-capas)
6. [Componentes Clave](#componentes-clave)
7. [Preguntas de Entrevista](#preguntas-de-entrevista)
8. [Casos de Uso Prácticos](#casos-de-uso-prácticos)

---

## 🏗️ **Arquitectura General**

### **Clean Architecture (Arquitectura Limpia)**
```
┌─────────────────────────────────────────────────┐
│                    API Layer                    │ ← Controllers, HTTP
├─────────────────────────────────────────────────┤
│                Application Layer                │ ← Commands, Queries, Handlers
├─────────────────────────────────────────────────┤
│                  Domain Layer                   │ ← Entidades, Value Objects, Reglas de Negocio
├─────────────────────────────────────────────────┤
│               Infrastructure Layer              │ ← Repositorios, Base de Datos, Servicios Externos
└─────────────────────────────────────────────────┘
```

### **Principios Aplicados:**
- **Dependency Inversion:** Las capas externas dependen de las internas
- **Single Responsibility:** Cada clase tiene una responsabilidad
- **Open/Closed:** Abierto para extensión, cerrado para modificación
- **Separation of Concerns:** Cada capa tiene su propósito específico

---

## 🛠️ **Tecnologías Utilizadas**

### **1. .NET 8**
- **¿Qué es?** Framework de desarrollo de Microsoft
- **¿Por qué?** 
  - Multiplataforma (Windows, Linux, macOS)
  - Alto rendimiento
  - Ecosistema maduro
  - Soporte a largo plazo (LTS)

### **2. ASP.NET Core**
- **¿Qué es?** Framework web para crear APIs REST
- **Características:**
  - Middleware pipeline configurable
  - Dependency Injection nativo
  - Soporte para OpenAPI/Swagger
  - Rendimiento optimizado

### **3. MediatR**
- **¿Qué es?** Librería que implementa el patrón Mediator
- **¿Para qué sirve?**
  - Desacopla controllers de la lógica de negocio
  - Centraliza el manejo de Commands y Queries
  - Permite agregar behaviors (validación, logging, etc.)
- **Ejemplo de uso:**
```csharp
// En el controller
var command = new CreateUserCommand(userData);
var result = await _mediator.Send(command); // ← MediatR encuentra el handler automáticamente
```

### **4. FluentValidation**
- **¿Qué es?** Librería para validaciones elegantes
- **Ventajas:**
  - Validaciones legibles y mantenibles
  - Separación de validaciones de la lógica de negocio
  - Mensajes de error personalizables
- **Ejemplo:**
```csharp
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserData.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email debe ser válido");
    }
}
```

### **5. AutoMapper**
- **¿Qué es?** Librería para mapear objetos automáticamente
- **¿Por qué?** Evita código repetitivo de mapeo manual
- **Ejemplo:**
```csharp
// Sin AutoMapper (manual)
var dto = new UserResponseDto
{
    Id = user.Id,
    Username = user.Username.Value,
    Email = user.Email.Value
};

// Con AutoMapper (automático)
var dto = _mapper.Map<UserResponseDto>(user);
```

### **6. BCrypt**
- **¿Qué es?** Algoritmo de hashing para contraseñas
- **¿Por qué?** 
  - Seguro contra ataques de fuerza bruta
  - Salt automático
  - Estándar de la industria

---

## 🎯 **Patrones de Diseño**

### **1. CQRS (Command Query Responsibility Segregation)**
- **Concepto:** Separar operaciones de lectura y escritura
- **Commands:** Modifican datos (Create, Update, Delete)
- **Queries:** Solo leen datos (Get, Search)

#### **Ventajas:**
- Optimización independiente de lectura y escritura
- Modelos específicos para cada operación
- Escalabilidad mejorada
- Código más mantenible

#### **Ejemplo en el proyecto:**
```csharp
// COMMAND (escribe)
public class CreateUserCommand : IRequest<UserResponseDto>
{
    public CreateUserRequestDto UserData { get; set; }
}

// QUERY (lee)
public class GetUserByIdQuery : IRequest<UserResponseDto?>
{
    public Guid UserId { get; set; }
}
```

### **2. Domain-Driven Design (DDD)**
- **Entidades:** Objetos con identidad única
- **Value Objects:** Objetos inmutables sin identidad
- **Aggregate Roots:** Entidades que controlan el acceso a otras entidades
- **Domain Services:** Lógica que no pertenece a una entidad específica

#### **Ejemplo en el proyecto:**
```csharp
// Entidad
public class User
{
    public Guid Id { get; private set; }
    public Username Username { get; private set; } // ← Value Object
    public Email Email { get; private set; }       // ← Value Object
}

// Value Object
public class Email
{
    public string Value { get; private set; }
    
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !IsValidEmail(value))
            throw new ValueObjectException("Email inválido");
        
        Value = value;
    }
}
```

### **3. Repository Pattern**
- **Propósito:** Abstraer el acceso a datos
- **Ventajas:**
  - Testeable (se puede mockear)
  - Intercambiable (in-memory, SQL, NoSQL)
  - Centraliza queries complejas

#### **Ejemplo:**
```csharp
// Interfaz en Domain (no sabe de implementación)
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task SaveAsync(User user);
}

// Implementación en Infrastructure
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    
    public Task<User?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }
}
```

### **4. Dependency Injection**
- **Propósito:** Inversión de control de dependencias
- **Ventajas:**
  - Código más testeable
  - Acoplamiento débil
  - Flexibilidad para cambiar implementaciones

---

## 🔄 **Flujo de Datos Completo**

### **Flujo de un Command (Escritura):**
```
1. HTTP POST → Controller
2. Controller → CreateUserCommand
3. MediatR → encuentra CreateUserCommandHandler
4. ValidationBehavior → valida con FluentValidation
5. Handler → crea entidad User
6. Handler → guarda en Repository
7. Handler → mapea con AutoMapper
8. Retorna → UserResponseDto
```

### **Flujo de una Query (Lectura):**
```
1. HTTP GET → Controller
2. Controller → GetUserByIdQuery
3. MediatR → encuentra GetUserByIdQueryHandler
4. Handler → busca en Repository
5. Handler → mapea con AutoMapper
6. Retorna → UserResponseDto
```

### **Ejemplo Práctico - Crear Usuario:**
```csharp
// 1. Request HTTP
POST /api/users
{
  "username": "juan123",
  "email": "juan@email.com",
  "password": "MiPassword123"
}

// 2. Controller recibe y crea Command
[HttpPost]
public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserRequestDto request)
{
    var command = new CreateUserCommand(request);
    var result = await _mediator.Send(command); // ← MediatR maneja todo
    return Created($"api/users/{result.Id}", result);
}

// 3. MediatR encuentra el Handler automáticamente
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 4. Validación automática (ValidationBehavior)
        // 5. Crear entidad User
        var user = new User(
            new Username(request.UserData.Username),
            new Email(request.UserData.Email),
            _passwordHasher.Hash(request.UserData.Password)
        );
        
        // 6. Guardar en repositorio
        await _userRepository.SaveAsync(user, cancellationToken);
        
        // 7. Mapear a DTO
        return _mapper.Map<UserResponseDto>(user);
    }
}
```

---

## 📁 **Estructura de Capas Detallada**

### **1. Domain Layer (Núcleo)**
```
📁 Domain/
├── 📁 Entities/          ← User, TaskItem, Category
├── 📁 ValueObjects/      ← Email, Username, TaskTitle
├── 📁 Enums/            ← TaskStatus, TaskPriority
├── 📁 Exceptions/       ← DomainException, ValueObjectException
└── 📁 Repositories/     ← IUserRepository, ITaskRepository
```

**Responsabilidades:**
- Lógica de negocio pura
- Reglas de dominio
- Validaciones de entidades
- Definir contratos (interfaces)

### **2. Application Layer (Casos de Uso)**
```
📁 Application/
├── 📁 Commands/         ← CreateUser, UpdateTask, DeleteCategory
├── 📁 Queries/          ← GetUserById, GetTasksByUser
├── 📁 DTOs/            ← Request/Response objects
├── 📁 Validators/       ← FluentValidation rules
├── 📁 Mappings/        ← AutoMapper profiles
├── 📁 Behaviors/       ← ValidationBehavior (Pipeline)
└── 📁 Services/        ← IPasswordHasher interface
```

**Responsabilidades:**
- Orquestar casos de uso
- Coordinar entre Domain e Infrastructure
- Transformar datos (DTOs)
- Validaciones de entrada

### **3. Infrastructure Layer (Implementaciones)**
```
📁 Infrastructure/
├── 📁 Repositories/     ← InMemoryUserRepository, EntityFrameworkUserRepository
├── 📁 Services/        ← BCryptPasswordHasher, EmailService
└── 📁 Data/            ← DbContext, Configurations
```

**Responsabilidades:**
- Implementar interfaces del Domain
- Acceso a base de datos
- Servicios externos (email, APIs)
- Configuraciones técnicas

### **4. API Layer (Entrada)**
```
📁 API/
├── 📁 Controllers/      ← UsersController, TasksController
└── Program.cs          ← Configuración DI, Middleware
```

**Responsabilidades:**
- Punto de entrada HTTP
- Configurar aplicación
- Manejo de errores HTTP
- Documentación (Swagger)

---

## 🔧 **Componentes Clave**

### **1. ValidationBehavior (Pipeline)**
```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 1. Ejecutar todas las validaciones
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        // 2. Si hay errores, lanzar excepción
        if (failures.Any())
            throw new ValidationException(failures);

        // 3. Si no hay errores, continuar al handler
        return await next();
    }
}
```

### **2. Value Objects**
```csharp
public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        // Validación en el constructor
        if (string.IsNullOrEmpty(value))
            throw new ValueObjectException("Email no puede estar vacío");
        
        if (!IsValidEmail(value))
            throw new ValueObjectException("Formato de email inválido");

        Value = value;
    }

    // Inmutabilidad: no hay setters públicos
    // Igualdad por valor, no por referencia
    public override bool Equals(object? obj) => obj is Email email && Value == email.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
```

### **3. AutoMapper Profiles**
```csharp
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // De Entidad a DTO
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

        // De DTO a Entidad (si fuera necesario)
        CreateMap<CreateUserRequestDto, User>()
            .ConstructUsing(src => new User(
                new Username(src.Username),
                new Email(src.Email),
                src.Password // Se hashea en el handler
            ));
    }
}
```

---

## ❓ **Preguntas de Entrevista**

### **🏗️ Arquitectura y Patrones**

**Q: ¿Por qué elegiste Clean Architecture para este proyecto?**
```
R: Clean Architecture ofrece:
- Separación clara de responsabilidades
- Testabilidad: cada capa se puede testear independientemente
- Mantenibilidad: cambios en una capa no afectan otras
- Escalabilidad: fácil agregar nuevas funcionalidades
- Independencia de frameworks: el dominio no depende de tecnologías específicas
```

**Q: ¿Cuál es la diferencia entre Commands y Queries en CQRS?**
```
R: 
- Commands: Modifican el estado (Create, Update, Delete). Pueden tener efectos secundarios.
- Queries: Solo leen datos, no modifican nada. Son idempotentes.
- Ventaja: Permite optimizar cada tipo de operación independientemente.
```

**Q: ¿Qué es un Value Object y cuándo lo usarías?**
```
R: Un Value Object es un objeto inmutable que se define por su valor, no por su identidad.
Características:
- No tiene ID
- Inmutable
- Igualdad por valor
- Contiene validaciones de dominio

Ejemplos: Email, Dinero, Dirección, Coordenadas GPS
```

### **🛠️ Tecnologías**

**Q: ¿Por qué usas MediatR en lugar de llamar directamente a los services?**
```
R: MediatR ofrece:
- Desacoplamiento: controllers no conocen handlers específicos
- Pipeline behaviors: validación, logging, caching automático
- Consistencia: todas las operaciones siguen el mismo patrón
- Testabilidad: fácil mockear
- Single Responsibility: cada handler hace una cosa
```

**Q: ¿Cuáles son las ventajas de usar AutoMapper?**
```
R: 
- Reduce código boilerplate de mapeo manual
- Configuración centralizada de mapeos
- Validación en tiempo de compilación de mapeos
- Performance optimizado
- Mapeos complejos con configuración fluent
```

**Q: ¿Por qué FluentValidation en lugar de DataAnnotations?**
```
R:
- Más expresivo y legible
- Validaciones complejas más fáciles
- Separación de responsabilidades
- Mensajes de error personalizables
- Validaciones condicionales
- Testeable independientemente
```

### **🔧 Implementación**

**Q: ¿Cómo manejas las transacciones en este proyecto?**
```
R: Actualmente uso repositorios in-memory, pero en producción:
- Unit of Work pattern para transacciones
- Repository pattern dentro de transacciones
- Entity Framework maneja transacciones automáticamente
- Para operaciones complejas, transacciones explícitas
```

**Q: ¿Cómo escalarias este proyecto para producción?**
```
R:
1. Base de datos real (SQL Server/PostgreSQL)
2. Entity Framework Core con migraciones
3. Cache (Redis) para queries frecuentes
4. Autenticación JWT
5. Logging estructurado (Serilog)
6. Health checks
7. API versioning
8. Rate limiting
9. Docker containerization
10. CI/CD pipeline
```

**Q: ¿Cómo manejarías errores en producción?**
```
R:
- Exception middleware global
- Logging estructurado con contexto
- Diferentes tipos de excepciones (Domain, Validation, Infrastructure)
- Códigos HTTP apropiados
- Mensajes de error consistentes
- No exponer detalles internos al cliente
```

### **🧪 Testing**

**Q: ¿Cómo testearías esta aplicación?**
```
R:
1. Unit Tests:
   - Domain entities y value objects
   - Handlers individualmente
   - Validators
   - Mappers

2. Integration Tests:
   - Controllers completos
   - Base de datos real
   - Pipeline completo

3. Mocking:
   - Repositories para unit tests
   - External services
   - IMediator para controller tests
```

**Q: ¿Qué herramientas de testing usarías?**
```
R:
- xUnit para framework de testing
- Moq para mocking
- FluentAssertions para assertions legibles
- TestContainers para integration tests
- AutoFixture para datos de prueba
- Coverlet para code coverage
```

### **🚀 Performance y Escalabilidad**

**Q: ¿Cómo optimizarias las queries de base de datos?**
```
R:
- Entity Framework: Include() para eager loading
- Projection: Select solo campos necesarios
- Pagination para listas grandes
- Indexing en campos de búsqueda frecuente
- Query splitting para relaciones complejas
- Cached queries para datos estáticos
```

**Q: ¿Cómo implementarías cache en esta arquitectura?**
```
R:
- IMemoryCache para cache local
- IDistributedCache (Redis) para cache distribuido
- Cache behavior en MediatR pipeline
- Cache invalidation en Commands
- Cache warming para datos críticos
```

---

## 📝 **Casos de Uso Prácticos**

### **Caso 1: Agregar nueva funcionalidad "Comentarios en Tareas"**

```
1. Domain Layer:
   - Crear entidad Comment
   - Agregar relación en TaskItem
   - Crear CommentText value object

2. Application Layer:
   - CreateCommentCommand + Handler
   - GetCommentsByTaskQuery + Handler
   - CommentRequestDto/ResponseDto
   - Validator para CreateComment
   - Mapping profile

3. Infrastructure Layer:
   - ICommentRepository interface
   - Implementación del repositorio

4. API Layer:
   - CommentsController
   - Endpoints REST
```

### **Caso 2: Migrar de In-Memory a SQL Server**

```
1. Infrastructure Layer:
   - Instalar Entity Framework Core
   - Crear DbContext
   - Configurar entities
   - Crear migraciones
   - Implementar EF repositories

2. API Layer:
   - Cambiar DI registration
   - Agregar connection string
   - Configurar DbContext

3. No cambios en:
   - Domain Layer
   - Application Layer
   ← Esto demuestra la flexibilidad de Clean Architecture
```

### **Caso 3: Agregar Autenticación JWT**

```
1. Application Layer:
   - LoginCommand + Handler
   - JWT service interface
   - User authentication logic

2. Infrastructure Layer:
   - JWT service implementation
   - User authentication repository methods

3. API Layer:
   - Authentication middleware
   - [Authorize] attributes
   - JWT configuration

4. Controllers:
   - AuthController para login/register
   - Proteger endpoints existentes
```

---

## 📊 **Métricas del Proyecto**

### **Complejidad:**
- **Líneas de código:** ~2,500 líneas
- **Clases:** ~45 clases
- **Proyectos:** 4 proyectos
- **Dependencias:** 6 NuGet packages principales

### **Cobertura de Funcionalidades:**
- ✅ **CRUD completo** para 3 entidades
- ✅ **15+ Commands y Queries**
- ✅ **12+ endpoints REST**
- ✅ **Validación automática**
- ✅ **Mapeo automático**
- ✅ **Documentación Swagger**

### **Patrones Implementados:**
- ✅ Clean Architecture
- ✅ CQRS
- ✅ Repository Pattern
- ✅ Mediator Pattern
- ✅ Value Object Pattern
- ✅ Dependency Injection
- ✅ Pipeline Pattern (Behaviors)

---

## 🎯 **Puntos Clave para Recordar**

### **Lo más importante:**
1. **Separación de responsabilidades:** Cada capa tiene su propósito
2. **Inversión de dependencias:** Domain no depende de Infrastructure
3. **CQRS:** Commands cambian datos, Queries los leen
4. **MediatR:** Desacopla y centraliza el manejo de requests
5. **Value Objects:** Encapsulan validaciones de dominio
6. **Repository Pattern:** Abstrae el acceso a datos

### **Tecnologías clave:**
- **.NET 8:** Framework base moderno
- **MediatR:** Patrón Mediator para desacoplamiento
- **FluentValidation:** Validaciones expresivas
- **AutoMapper:** Mapeo automático de objetos
- **ASP.NET Core:** API REST robusta

### **Beneficios de esta arquitectura:**
- **Mantenible:** Fácil de modificar y extender
- **Testeable:** Cada componente se puede testear independientemente
- **Escalable:** Preparado para crecer
- **Profesional:** Sigue las mejores prácticas de la industria

---

## 🚀 **Próximos Pasos (Mejoras Sugeridas)**

### **Básicas:**
1. Tests unitarios e integración
2. Base de datos real (Entity Framework)
3. Manejo de errores global
4. Logging estructurado

### **Intermedias:**
5. Autenticación JWT
6. Autorización basada en roles
7. Paginación en queries
8. Cache con Redis

### **Avanzadas:**
9. Event Sourcing
10. CQRS con bases de datos separadas
11. Microservicios
12. Docker + Kubernetes

---

## 💡 **Consejos para la Entrevista**

1. **Prepara ejemplos concretos:** Conoce bien el flujo de crear un usuario
2. **Entiende los "por qué":** No solo cómo funciona, sino por qué elegiste cada tecnología
3. **Discute trade-offs:** Menciona las ventajas y desventajas de cada decisión
4. **Piensa en escalabilidad:** Cómo crecería el proyecto en producción
5. **Demuestra conocimiento:** Habla de patrones alternativos que consideraste

**¡Recuerda!** Este proyecto demuestra conocimiento sólido de arquitectura moderna y patrones de la industria. ¡Estás bien preparado! 🎯
