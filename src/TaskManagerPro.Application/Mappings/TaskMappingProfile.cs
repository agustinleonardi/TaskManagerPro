using AutoMapper;
using TaskManagerPro.Application.DTOs.Task;
using TaskManagerPro.Domain.Entities;
using TaskManagerPro.Domain.Enums;
using DomainTaskStatus = TaskManagerPro.Domain.Enums.TaskStatus;
using DomainTaskPriority = TaskManagerPro.Domain.Enums.TaskPriority;

namespace TaskManagerPro.Application.Mappings;

/// <summary>
/// Perfil de mapeo para TaskItem
/// </summary>
public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        // TaskItem Entity → TaskResponseDto
        CreateMap<TaskItem, TaskResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.ToString()))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.CompletedAt))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
            .ForMember(dest => dest.IsInProgress, opt => opt.MapFrom(src => src.IsInProgress))
            .ForMember(dest => dest.IsPending, opt => opt.MapFrom(src => src.IsPending));
    }
}
