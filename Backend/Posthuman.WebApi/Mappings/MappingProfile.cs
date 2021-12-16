using AutoMapper;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();
            CreateMap<CreateTodoItemDTO, TodoItem>();

            CreateMap<TodoItemCycle, TodoItemCycleDTO>();
            CreateMap<TodoItemCycleDTO, TodoItemCycle>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<Avatar, AvatarDTO>();
            CreateMap<AvatarDTO, Avatar>();

            CreateMap<EventItem, EventItemDTO>();
            CreateMap<EventItemDTO, EventItem>();

            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<BlogPostDTO, BlogPost>();

            CreateMap<Requirement, RequirementDTO>();
            CreateMap<RequirementDTO, Requirement>();

            CreateMap<TechnologyCard, TechnologyCardDTO>();
            CreateMap<TechnologyCardDTO, TechnologyCard>();

            CreateMap<TechnologyCardDiscovery, TechnologyCardDiscoveryDTO>();
            CreateMap<TechnologyCardDiscoveryDTO, TechnologyCardDiscovery>();
        }
    }
}
