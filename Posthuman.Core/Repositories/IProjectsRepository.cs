using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Repositories
{
    public interface IProjectsRepository : IRepository<Project>
    {
        public Task<IEnumerable<Project>> GetAllByAvatarId(int id);
        Task<IEnumerable<TodoItemDTO>> GetAllWithSubtasksAsync();
        Task<TodoItemDTO> GetByIdWithSubtasksAsync(int id);
    }
}