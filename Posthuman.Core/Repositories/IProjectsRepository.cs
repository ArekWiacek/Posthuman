using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Repositories
{
    public interface IProjectsRepository : IRepository<Project>
    {
        Task<IEnumerable<TodoItemDTO>> GetAllWithSubtasksAsync();
        Task<TodoItemDTO> GetByIdWithSubtasksAsync(int id);
    }
}