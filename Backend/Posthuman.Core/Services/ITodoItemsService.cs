using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface ITodoItemsService
    {
        Task<TodoItemDTO> GetTodoItemById(int id);
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems(int userId);
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsHierarchical(int userId);

        
        Task<TodoItemDTO> CreateTodoItem(int userId, CreateTodoItemDTO todoItemDTO);
        
        Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        
        Task DeleteTodoItem(int id);

        Task CompleteTodoItem(TodoItemDTO todoITemDTO);
    }
}
