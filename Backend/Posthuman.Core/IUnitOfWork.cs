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
        IAvatarsRepository Avatars { get; }
        ITodoItemsRepository TodoItems { get; }
        IHabitsRepository Habits { get; }
        IEventItemsRepository EventItems { get; }
        IBlogPostsRepository BlogPosts { get; }
        IRequirementsRepository Requirements { get;  }
        ITechnologyCardsRepository TechnologyCards { get; }
        ITechnologyCardsDiscoveriesRepository TechnologyCardsDiscoveries { get; }

        Task<int> CommitAsync();
    }
}
