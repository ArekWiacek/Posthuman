using AutoMapper;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

//            var configuration = new MapperConfiguration(cfg =>
//            {
//                cfg.AllowNullCollections = true;
//                cfg.CreateMap<TodoItem, TodoItemDTO>();
//            });

//            CreateMap<TodoItem, TodoItemDTO>()
//.ForMember(t => t.Subtasks, options => options.MapFrom(source => source.Subtasks));

            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<Avatar, AvatarDTO>();
            CreateMap<AvatarDTO, Avatar>();

            CreateMap<EventItem, EventItemDTO>();
            CreateMap<EventItemDTO, EventItem>();
        }
    //        var configuration = new MapperConfiguration(c => {
    //            c.CreateMap<TodoItem, TodoItemm>()
    //                 .Include<ChildSource, ChildDestination>();
    //            c.CreateMap<ChildSource, ChildDestination>();
    //        });

    //        var sources = new[]
    //            {
    //    new ParentSource(),
    //    new ChildSource(),
    //    new ParentSource()
    //};

    //        var destinations = mapper.Map<ParentSource[], ParentDestination[]>(sources);

    //        destinations[0].ShouldBeInstanceOf<ParentDestination>();
    //        destinations[1].ShouldBeInstanceOf<ChildDestination>();
    //        destinations[2].ShouldBeInstanceOf<ParentDestination>();
    //    }
    }
}
