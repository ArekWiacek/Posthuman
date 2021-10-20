using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Services
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems();
        Task<TodoItemDTO> GetTodoItemById(int id);

        Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsForActiveAvatar();
        // Task<TodoItemDTO> GetTodoItemByIdForActiveAvatar(int id);

        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        Task DeleteTodoItem(int id);

        // Not sure
        // Task DeleteTodoItem(TodoItem todoItem);
        Task<IEnumerable<TodoItem>> GetByParentProject(int projectId);
    }
}
