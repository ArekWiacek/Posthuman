using AutoMapper;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<Avatar, AvatarDTO>();
            CreateMap<AvatarDTO, Avatar>();

            CreateMap<EventItem, EventItemDTO>();
            CreateMap<EventItemDTO, EventItem>();

            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<BlogPostDTO, BlogPost>();
        }
    }
}
