using Posthuman.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface ITodoItemsService
    {
        Task<TodoItemDTO> GetTodoItemById(int id, int userId);
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems(int userId);
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItemsHierarchical(int userId);
        Task <IEnumerable<TodoItemDTO>> GetTodoItemsByDeadline(int userId, DateTime deadline);


        Task<TodoItemDTO> CreateTodoItem(int userId, CreateTodoItemDTO todoItemDTO);
        
        Task UpdateTodoItem(int userId, TodoItemDTO todoItemDTO);
        
        Task DeleteTodoItem(int id, int userId);

        Task CompleteTodoItem(int id, int userId);
    }
}
