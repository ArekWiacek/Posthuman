using Posthuman.Core.Repositories;

namespace Posthuman.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemsRepository TodoItems { get; }
        IProjectsRepository Projects { get; }
        IEventItemsRepository EventItems { get; }
        IAvatarsRepository Avatars { get; }
        Task<int> CommitAsync();
    }
}
