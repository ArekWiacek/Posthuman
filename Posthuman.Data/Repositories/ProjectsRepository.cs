using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.DTO;

namespace Posthuman.Data.Repositories
{
    public class ProjectsRepository : Repository<Project>, IProjectsRepository
    {
        public ProjectsRepository(PosthumanContext context) : base(context)
        {

        }

        private PosthumanContext ProjectsDbContext
        {
            get { return Context as PosthumanContext; }
        }

        public async Task<IEnumerable<TodoItem>> GetAllWithSubtasksAsync()
        {
            return (IEnumerable<TodoItem>)await ProjectsDbContext
                .Projects
                .Include(p => p.TodoItems)
                .ToListAsync();
        }

        public Task<TodoItemDTO> GetByIdWithSubtasksAsync(int id)
        {
            throw new NotImplementedException();
        }
        Task<IEnumerable<TodoItemDTO>> IProjectsRepository.GetAllWithSubtasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
