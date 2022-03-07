using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class TodoItemsCyclesRepository : Repository<TodoItemCycle>, ITodoItemsCyclesRepository
    {
        public TodoItemsCyclesRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext TodoItemsDbContext
        {
            get { return Context; }
        }
    }
}
