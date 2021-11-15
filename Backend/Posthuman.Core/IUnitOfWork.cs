using Posthuman.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Posthuman.Core
{
    /// <summary>
    /// Unit of work - contains all repositiories and handles updating many data models in single transaction
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemsRepository TodoItems { get; }
        IProjectsRepository Projects { get; }
        IEventItemsRepository EventItems { get; }
        IAvatarsRepository Avatars { get; }

        Task<int> CommitAsync();
    }
}
