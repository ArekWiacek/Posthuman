using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Data.Repositories;
using System.Threading.Tasks;

namespace Posthuman.Data
{
    // This class contains all repositories 
    // Using it allows to manipulate different entities in one shot
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosthumanContext context;
        private UsersRepository usersRepository;
        private TodoItemsRepository todoItemsRepository;
        private TodoItemsCyclesRepository todoItemsCyclesRepository;
        private ProjectsRepository projectsRepository;
        private EventItemsRepository eventItemsRepository;
        private AvatarsRepository avatarsRepository;
        private BlogPostsRepository blogPostsRepository;
        private TechnologyCardsRepository technologyCardsRepository;
        private TechnologyCardsDiscoveriesRepository technologyCardsDiscoveriesRepostitory;
        private RequirementsRepository requirementsRepository;

        public UnitOfWork(PosthumanContext context)
        {
            this.context = context;
            usersRepository             = usersRepository           ?? new UsersRepository(context);
            todoItemsRepository         = todoItemsRepository       ?? new TodoItemsRepository(context);
            todoItemsCyclesRepository   = todoItemsCyclesRepository ?? new TodoItemsCyclesRepository(context);
            projectsRepository          = projectsRepository        ?? new ProjectsRepository(context);
            eventItemsRepository        = eventItemsRepository      ?? new EventItemsRepository(context);
            todoItemsRepository         = todoItemsRepository       ?? new TodoItemsRepository(context);
            avatarsRepository           = avatarsRepository         ?? new AvatarsRepository(context);
            blogPostsRepository         = blogPostsRepository       ?? new BlogPostsRepository(context);
            technologyCardsRepository   = technologyCardsRepository ?? new TechnologyCardsRepository(context);
            requirementsRepository      = requirementsRepository    ?? new RequirementsRepository(context);
            technologyCardsDiscoveriesRepostitory = technologyCardsDiscoveriesRepostitory ?? new TechnologyCardsDiscoveriesRepository(context);
        }

        public IUsersRepository Users                       => usersRepository              = usersRepository           ??  new UsersRepository(context);
        public ITodoItemsRepository TodoItems               => todoItemsRepository          = todoItemsRepository       ??  new TodoItemsRepository(context);
        public ITodoItemsCyclesRepository TodoItemsCycles   => todoItemsCyclesRepository    = todoItemsCyclesRepository ??  new TodoItemsCyclesRepository(context);
        public IProjectsRepository Projects                 => projectsRepository           = projectsRepository        ??  new ProjectsRepository(context);
        public IEventItemsRepository EventItems             => eventItemsRepository         = eventItemsRepository      ??  new EventItemsRepository(context);
        public IAvatarsRepository Avatars                   => avatarsRepository            = avatarsRepository         ??  new AvatarsRepository(context);
        public IBlogPostsRepository BlogPosts               => blogPostsRepository          = blogPostsRepository       ??  new BlogPostsRepository(context);
        public ITechnologyCardsRepository TechnologyCards   => technologyCardsRepository    = technologyCardsRepository ??  new TechnologyCardsRepository(context);
        public IRequirementsRepository Requirements         => requirementsRepository       = requirementsRepository    ??  new RequirementsRepository(context);
        public ITechnologyCardsDiscoveriesRepository TechnologyCardsDiscoveries => technologyCardsDiscoveriesRepostitory = technologyCardsDiscoveriesRepostitory ?? new TechnologyCardsDiscoveriesRepository(context);
        
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
