using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems();
        Task<TodoItemDTO> GetTodoItemById(int id);

        Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsForActiveAvatar();
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsHierarchical();
        
        
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        Task DeleteTodoItem(int id);

        Task CompleteTodoItem(TodoItemDTO todoITemDTO);
    }
}
