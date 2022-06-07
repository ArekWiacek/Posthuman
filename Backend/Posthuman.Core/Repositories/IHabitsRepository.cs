using Posthuman.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Repository interface for managing entities of type Habit
    /// Interface will contain operations specific to this entity type
    /// </summary>
    public interface IHabitsRepository : IRepository<Habit>
    {
        public Task<Habit> GetByUserId(int id, int userId);
        public Task<IEnumerable<Habit>> GetAllByUserIdAsync(int userId);
    }
}