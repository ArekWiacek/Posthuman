﻿using System.Collections.Generic;
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

        public async Task<IEnumerable<TodoItem>> GetAllByAvatarIdAsync(int id)
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
            return await TodoItemsDbContext
                 .TodoItems
                 .Where(ti => ti.Id == todoItemId)
                 .Include(ti => ti.Subtasks)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllByParentIdAsync(int id)
        {
            return await TodoItemsDbContext
                .TodoItems
                .Include(ti => ti.Subtasks)
                .Where(ti => ti.ParentId == id)
                .ToListAsync();
        }
    }
}
