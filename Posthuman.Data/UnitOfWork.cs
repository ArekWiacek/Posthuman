using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Data.Repositories;

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

        public UnitOfWork(PosthumanContext context)
        {
            this.context = context;
        }

        public ITodoItemsRepository TodoItems => todoItemsRepository = todoItemsRepository ?? new TodoItemsRepository(context);

        public IProjectsRepository Projects => projectsRepository = projectsRepository ?? new ProjectsRepository(context);

        public IEventItemsRepository EventItems => eventItemsRepository = eventItemsRepository ?? new EventItemsRepository(context);

        public IAvatarsRepository Avatars => avatarsRepository = avatarsRepository ?? new AvatarsRepository(context);

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
