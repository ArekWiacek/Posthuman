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
        private TodoItemsRepository todoItemsRepository;
        private ProjectsRepository projectsRepository;
        private EventItemsRepository eventItemsRepository;
        private AvatarsRepository avatarsRepository;
        private BlogPostsRepository blogPostsRepository;
        private RewardCardsRepository rewardCardsRepository;

        public UnitOfWork(PosthumanContext context)
        {
            this.context = context;
            todoItemsRepository     = todoItemsRepository   ?? new TodoItemsRepository(context);
            projectsRepository      = projectsRepository    ?? new ProjectsRepository(context);
            eventItemsRepository    = eventItemsRepository  ?? new EventItemsRepository(context);
            todoItemsRepository     = todoItemsRepository   ?? new TodoItemsRepository(context);
            avatarsRepository       = avatarsRepository     ?? new AvatarsRepository(context);
            blogPostsRepository     = blogPostsRepository   ?? new BlogPostsRepository(context);
            rewardCardsRepository   = rewardCardsRepository ?? new RewardCardsRepository(context);
        }

        public ITodoItemsRepository TodoItems       => todoItemsRepository  = todoItemsRepository       ??  new TodoItemsRepository(context);
        public IProjectsRepository Projects         => projectsRepository   = projectsRepository        ??  new ProjectsRepository(context);
        public IEventItemsRepository EventItems     => eventItemsRepository = eventItemsRepository      ??  new EventItemsRepository(context);
        public IAvatarsRepository Avatars           => avatarsRepository    = avatarsRepository         ??  new AvatarsRepository(context);
        public IBlogPostsRepository BlogPosts       => blogPostsRepository  = blogPostsRepository       ??  new BlogPostsRepository(context);
        public IRewardCardsRepository RewardCards   => rewardCardsRepository = rewardCardsRepository    ??  new RewardCardsRepository(context);

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
