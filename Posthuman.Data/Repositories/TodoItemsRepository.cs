using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Posthuman.Data.Repositories
{
    public class TodoItemsRepository : Repository<TodoItem>, ITodoItemsRepository
    {
        public TodoItemsRepository(PosthumanContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetAllByAvatarId(int id)
        {
            return await TodoItemsDbContext
                .TodoItems
                .Where(ti => ti.AvatarId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllWithProjectsAsync()
        {
            return await TodoItemsDbContext
                .TodoItems
                .Include(ti => ti.Project)
                .ToListAsync();
        }

        Task<IEnumerable<TodoItem>> ITodoItemsRepository.GetAllWithProjectsAsync()
        {
            throw new NotImplementedException();
        }

        //public Task<TodoItem?> GetByIdAsync(int id)
        //{
        //    return TodoItemsDbContext
        //        .TodoItems
        //        .SingleOrDefaultAsync(ti => ti.Id == id);
        //}

        //public Task<TodoItem?> GetWithProjectByIdAsync(int id)
        //{
        //    return TodoItemsDbContext
        //        .TodoItems
        //        .Include(ti => ti.Project)
        //        .SingleOrDefaultAsync(ti => ti.Id == id);
        //}

        private PosthumanContext TodoItemsDbContext
        {
            get { return Context as PosthumanContext; }
        }
    }
}
