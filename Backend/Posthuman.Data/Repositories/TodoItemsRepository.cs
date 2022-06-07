using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class TodoItemsRepository : Repository<TodoItem>, ITodoItemsRepository
    {
        public TodoItemsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext TodoItemsDbContext
        {
            get { return Context; }
        }

        public async Task<TodoItem> GetByUserId(int id, int userId)
        {
            return await TodoItemsDbContext
                .TodoItems
                .Where(ti => ti.Id == id & ti.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(int userId)
        {
            return await TodoItemsDbContext
                .TodoItems
                .Where(ti => ti.UserId == userId)
                .ToListAsync();
        }

        public async Task<TodoItem> GetByIdWithSubtasksAsync(int todoItemId)
        {
            return await TodoItemsDbContext
                 .TodoItems
                 .Where(ti => ti.Id == todoItemId)
                 .Include(ti => ti.Subtasks)
                 //.Include(ti => ti.Avatar)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllByParentIdAsync(int id)
        {
            return await TodoItemsDbContext
                .TodoItems
                //.Include(ti => ti.Avatar)
                .Include(ti => ti.Subtasks)
                .Where(ti => ti.ParentId == id)
                .ToListAsync();
        }

    }
}
