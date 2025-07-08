using AutoMapper;
using TaskManagerPro.Application.DTOs.Category;
using TaskManagerPro.Domain.Entities;

namespace TaskManagerPro.Application.Mappings;

/// <summary>
/// Perfil de mapeo para Category
/// </summary>
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        // Category Entity → CategoryResponseDto
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
    }
}
