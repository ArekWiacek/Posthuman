using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

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

        public async Task<TodoItem> GetByIdWithSubtasksAsync(int todoItemId)
        {
            var coll1 = await TodoItemsDbContext
                .TodoItems
                .Include(ti => ti.Subtasks)
                .Where(ti => ti.Id == todoItemId)
                .FirstOrDefaultAsync();

            var coll2 = await TodoItemsDbContext
             .TodoItems
             .Where(ti => ti.Id == todoItemId)
             .Include(ti => ti.Subtasks)
             .FirstOrDefaultAsync();

            return coll1;
        }
    

        public async Task<IEnumerable<TodoItem>> GetAllByParentIdAsync(int id)
        {
            return await TodoItemsDbContext
                .TodoItems
                .Include(ti => ti.Subtasks)
                .Where(ti => ti.ParentId == id)
                .ToListAsync();
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
