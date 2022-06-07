using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class HabitsRepository : Repository<Habit>, IHabitsRepository
    {
        public HabitsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext HabitsDbContext
        {
            get { return Context; }
        }

        public new async ValueTask<Habit> GetByIdAsync(int habitId, int userId)
        {
            return await HabitsDbContext
                   .Habits
                   //.Include(h => h.CompletedInstancesInfo)
                   .Where(h => h.Id == habitId & h.UserId == userId)
                   .FirstOrDefaultAsync();
        }

        public async Task<Habit> GetByUserId(int id, int userId)
        {
            return await HabitsDbContext
                   .Habits
                   .Where(h => h.Id == id & h.UserId == userId)
                   .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Habit>> GetAllByUserIdAsync(int userId)
        {
            var habits = 
                await HabitsDbContext
                .Habits
                .Where(h => h.UserId == userId).ToListAsync();

            return habits;
        }

    }
}
