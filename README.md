# 🚀 TaskManagerPro

Una aplicación de gestión de tareas desarrollada con **Clean Architecture**, **CQRS**, **MediatR** y **Domain-Driven Design (DDD)**.

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

### 📁 Estructura del Proyecto

```
TaskManagerPro_V2/
├── src/
│   ├── TaskManagerPro.Domain/        # 🎯 Lógica de negocio pura
│   │   ├── Entities/                 # Entidades del dominio
│   │   ├── ValueObjects/             # Objetos de valor
│   │   ├── Enums/                    # Enumeraciones del dominio
│   │   ├── Repositories/             # Interfaces de repositorio
│   │   └── Exceptions/               # Excepciones del dominio
│   │
│   ├── TaskManagerPro.Application/   # 🎪 Casos de uso y orquestación
│   │   ├── Commands/                 # Comandos CQRS
│   │   ├── Queries/                  # Consultas CQRS
│   │   ├── DTOs/                     # Data Transfer Objects
│   │   ├── Validators/               # Validadores FluentValidation
│   │   ├── Mappings/                 # Perfiles AutoMapper
│   │   ├── Behaviors/                # Pipelines MediatR
│   │   └── Services/                 # Interfaces de servicios
│   │
│   ├── TaskManagerPro.Infrastructure/ # ⚙️ Implementaciones técnicas
│   │   ├── Repositories/             # Implementaciones de repositorio
│   │   ├── Services/                 # Servicios de infraestructura
│   │   └── Data/                     # Contexto de base de datos
│   │
│   └── TaskManagerPro.API/           # 🌐 Punto de entrada HTTP
│       ├── Controllers/              # Controladores REST
│       └── Program.cs                # Configuración de la aplicación
└── tests/                            # 🧪 Pruebas unitarias e integración
```

## 🚀 Tecnologías Utilizadas

### **Backend**
- **.NET 8** - Framework principal
- **ASP.NET Core** - API REST
- **MediatR** - Patrón Mediator y CQRS
- **FluentValidation** - Validaciones
- **AutoMapper** - Mapeo de objetos
- **BCrypt.Net** - Hash de contraseñas

### **Patrones y Principios**
- **Clean Architecture** - Arquitectura por capas
- **CQRS** (Command Query Responsibility Segregation)
- **Domain-Driven Design (DDD)**
- **Repository Pattern**
- **Dependency Injection**
- **SOLID Principles**

## 🎯 Características Principales

### **🏗️ Domain Layer**
- **Entidades**: `User`, `TaskItem`, `Category`
- **Value Objects**: `Email`, `Username`, `TaskTitle`, `TaskDescription`
- **Enums**: `TaskStatus`, `TaskPriority`, `CategoryType`
- **Validaciones de dominio** integradas en entidades y value objects

### **🎪 Application Layer**
- **Commands**: `CreateUserCommand`, `CreateTaskCommand`, etc.
- **Queries**: `GetUserByIdQuery`, `GetTasksByUserQuery`, etc.
- **Validation Pipeline** automático con FluentValidation
- **AutoMapper** para transformaciones DTO ↔ Entity

### **⚙️ Infrastructure Layer**
- **BCrypt** para hash seguro de contraseñas
- **Repository implementations** (preparado para EF Core)
- **Servicios externos** (email, storage, etc.)

### **🌐 API Layer**
- **Controllers** REST con documentación Swagger
- **Dependency Injection** configurado
- **Error handling** centralizado
- **Validation responses** estructuradas

## 📋 Endpoints Principales

### **👤 Users**
```http
POST   /api/users           # Crear usuario
GET    /api/users/{id}      # Obtener usuario por ID
PUT    /api/users/{id}      # Actualizar usuario
DELETE /api/users/{id}      # Eliminar usuario
```

### **📋 Tasks**
```http
POST   /api/tasks           # Crear tarea
GET    /api/tasks/{id}      # Obtener tarea por ID
GET    /api/tasks/user/{userId}  # Obtener tareas de usuario
PUT    /api/tasks/{id}      # Actualizar tarea
DELETE /api/tasks/{id}      # Eliminar tarea
```

### **🏷️ Categories**
```http
GET    /api/categories      # Listar todas las categorías
GET    /api/categories/{id} # Obtener categoría por ID
POST   /api/categories      # Crear categoría
PUT    /api/categories/{id} # Actualizar categoría
DELETE /api/categories/{id} # Eliminar categoría
```

## 🚀 Cómo Ejecutar

### **Prerrequisitos**
- .NET 8 SDK
- IDE (Visual Studio, VS Code, Rider)

### **Pasos**
1. **Clonar el repositorio**
   ```bash
   git clone [tu-repo-url]
   cd TaskManagerPro_V2
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Ejecutar la aplicación**
   ```bash
   cd src/TaskManagerPro.API
   dotnet run
   ```

4. **Acceder a Swagger**
   ```
   https://localhost:7xxx/swagger
   ```

## 🏗️ Estado del Proyecto

### ✅ **COMPLETADO**
- **Domain Layer** completo con entidades, value objects y validaciones
- **Application Layer** con CQRS, MediatR, FluentValidation y AutoMapper
- **Infrastructure Layer** con repositorios in-memory y servicios
- **API Layer** con controllers REST y documentación Swagger
- **Dependency Injection** completamente configurado
- **Validation Pipeline** automático funcionando
- **Compilación exitosa** y aplicación ejecutándose

## 🧪 Testing

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 🏗️ Próximas Implementaciones

- [ ] **Entity Framework Core** integración
- [ ] **Base de datos** (SQL Server/PostgreSQL)
- [ ] **JWT Authentication**
- [ ] **Logging** con Serilog
- [ ] **Caching** con Redis
- [ ] **Tests unitarios** completos
- [ ] **CI/CD Pipeline**
- [ ] **Docker** containerización

## 📚 Patrones Implementados

### **🎯 CQRS (Command Query Responsibility Segregation)**
Separación clara entre operaciones de escritura (Commands) y lectura (Queries).

### **🎪 Mediator Pattern**
Desacoplamiento total entre Controllers y lógica de negocio usando MediatR.

### **🔍 Repository Pattern**
Abstracción del acceso a datos con interfaces en Domain e implementaciones en Infrastructure.

### **🛡️ Validation Pipeline**
Validaciones automáticas antes de ejecutar cualquier comando usando FluentValidation.

### **🗺️ AutoMapper**
Transformaciones automáticas entre DTOs y Entidades del dominio.

## 👨‍💻 Autor

**Agustín Leonardi**
- LinkedIn: [tu-linkedin]
- GitHub: [tu-github]
- Email: [tu-email]

## 📄 Licencia

Este proyecto es de código abierto bajo la licencia MIT.

---

⭐ **¡Si te gusta este proyecto, dale una estrella!** ⭐
