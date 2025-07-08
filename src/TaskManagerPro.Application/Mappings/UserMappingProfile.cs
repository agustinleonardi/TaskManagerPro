using AutoMapper;
using TaskManagerPro.Application.Commands;
using TaskManagerPro.Application.Commands.User;
using TaskManagerPro.Application.DTOs;
using TaskManagerPro.Application.DTOs.User;
using TaskManagerPro.Domain.Entities;

namespace TaskManagerPro.Application.Mappings;

/// <summary>
/// Perfil de mapeo para User
/// </summary>
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // User Entity → UserResponseDto
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.TaskCount, opt => opt.MapFrom(src => src.Tasks.Count))
            .ForMember(dest => dest.CompletedTasksCount, opt => opt.MapFrom(src => src.GetCompletedTasksCount()));

        // CreateUserRequestDto → CreateUserCommand (si lo necesitamos)

    }
}
