using FluentValidation;
using MediatR;
using System.Reflection;
using TaskManagerPro.Application.Behaviors;
using TaskManagerPro.Application.Commands.User;
using TaskManagerPro.Application.Mappings;
using TaskManagerPro.Application.Services;
using TaskManagerPro.Application.Validators;
using TaskManagerPro.Infrastructure.Services;
using TaskManagerPro.Infrastructure.Repositories;
using TaskManagerPro.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "TaskManagerPro API", 
        Version = "v1",
        Description = "API REST para gestión de tareas con Clean Architecture y CQRS"
    });
});

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

// AutoMapper
builder.Services.AddAutoMapper(
    typeof(UserMappingProfile).Assembly,
    typeof(TaskMappingProfile).Assembly,
    typeof(CategoryMappingProfile).Assembly
);

// Pipeline Behaviors
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Application Services
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

// Repository implementations (In-Memory for now)
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagerPro API V1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
