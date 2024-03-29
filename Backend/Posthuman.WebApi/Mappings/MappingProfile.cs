﻿using AutoMapper;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Models.Entities;

namespace Posthuman.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapUserModels();
            MapAvatarModels();
            MapTodoItemModels();
            MapProjectModels();
            MapEventItemModels();
            MapBlogPostModels();
            MapRequirementModels();
            MapTechnologyCardModels();
        }

        private void MapUserModels()
        {
            CreateMap<User, RegisterUserDTO>();
            CreateMap<User, LoginUserDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<LoginUserDTO, User>();
            CreateMap<UserDTO, User>();
        }

        private void MapAvatarModels()
        {
            CreateMap<Avatar, AvatarDTO>();
            CreateMap<UpdateAvatarDTO, Avatar>();
            CreateMap<AvatarDTO, Avatar>();
        }
        
        private void MapTodoItemModels()
        {
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();
            CreateMap<CreateTodoItemDTO, TodoItem>();

            CreateMap<TodoItemCycle, TodoItemCycleDTO>();
            CreateMap<TodoItemCycleDTO, TodoItemCycle>();
        }

        private void MapProjectModels()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();
        }
        
        private void MapEventItemModels()
        {
            CreateMap<EventItem, EventItemDTO>();
            CreateMap<EventItemDTO, EventItem>();
        }

        private void MapBlogPostModels()
        {
            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<BlogPostDTO, BlogPost>();
        }

        private void MapRequirementModels()
        {
            CreateMap<Requirement, RequirementDTO>();
            CreateMap<RequirementDTO, Requirement>();
        }

        private void MapTechnologyCardModels()
        {
            CreateMap<TechnologyCard, TechnologyCardDTO>();
            CreateMap<TechnologyCardDTO, TechnologyCard>();

            CreateMap<TechnologyCardDiscovery, TechnologyCardDiscoveryDTO>();
            CreateMap<TechnologyCardDiscoveryDTO, TechnologyCardDiscovery>();
        }
    }
}
