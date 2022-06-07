using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Repository interface for managing entities of type Habit
    /// Interface will contain operations specific to this entity type
    /// </summary>
    public interface IHabitsCompletionsRepository : IRepository<HabitCompletion>
    {
        //public Task<Completion> Get(int habitId, int userId);
        //public Task<IEnumerable<Habit>> GetAllByUserIdAsync(int userId);
    }
}