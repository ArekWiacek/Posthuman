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

        public async Task<IEnumerable<Habit>> GetAllByUserIdAsync(int userId)
        {
            return await HabitsDbContext
                .Habits
                .Where(h => h.UserId == userId)
                .ToListAsync();
        }
    }
}
