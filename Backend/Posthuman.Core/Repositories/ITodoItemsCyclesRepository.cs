using Posthuman.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Repository interface for managing entities of type TodoItem
    /// Interface will contain operations specific to this entity type
    /// </summary>
    public interface ITodoItemsCyclesRepository : IRepository<TodoItemCycle>
    {
        //public Task<IEnumerable<TodoItem>> GetAllByAvatarIdAsync(int id);

        //public Task<IEnumerable<TodoItem>> GetAllByParentIdAsync(int id);

        //public Task<TodoItem> GetByIdWithSubtasksAsync(int todoItemId);
    }
}