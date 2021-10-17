using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Repository interface for managing entities of type TodoItem
    /// Interface will contain operations specific to this entity type
    /// </summary>
    public interface ITodoItemsRepository : IRepository<TodoItem>
    {
        public Task<IEnumerable<TodoItem>> GetAllWithProjectsAsync();
    }
}