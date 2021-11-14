using Posthuman.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Repository interface for managing entities of type TodoItem
    /// Interface will contain operations specific to this entity type
    /// </summary>
    public interface ITodoItemsRepository : IRepository<TodoItem>
    {
        public Task<IEnumerable<TodoItem>> GetAllByAvatarId(int id);

        public Task<IEnumerable<TodoItem>> GetAllByParentIdAsync(int id);

        public Task<TodoItem> GetByIdWithSubtasksAsync(int todoItemId);

        public Task<IEnumerable<TodoItem>> GetAllWithProjectsAsync();
    }
}