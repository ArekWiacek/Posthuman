using Posthuman.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Posthuman.Core
{
    /// <summary>
    /// Unit of work - contains all repositiories and handles updating multiple data models in single transaction
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        ITodoItemsRepository TodoItems { get; }
        IProjectsRepository Projects { get; }
        IEventItemsRepository EventItems { get; }
        IAvatarsRepository Avatars { get; }
        IBlogPostsRepository BlogPosts { get; }
        IRequirementsRepository Requirements { get;  }
        ITechnologyCardsRepository TechnologyCards { get; }
        ITechnologyCardsDiscoveriesRepository TechnologyCardsDiscoveries { get; }
        IHabitsRepository Habits { get; }

        Task<int> CommitAsync();
    }
}
