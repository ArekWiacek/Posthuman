using System.Collections.Generic;
using System.Threading.Tasks;
using Posthuman.Core.Models.DTO.Habit;

namespace Posthuman.Core.Services
{
    public interface IHabitsService
    {
        Task<HabitDTO> GetByUserId(int id, int userId);
        Task<IEnumerable<HabitDTO>> GetAllByUserId(int userId);

        Task<HabitDTO> CreateHabit(int userId, CreateHabitDTO habitDTO);
        
        Task UpdateHabit(CreateHabitDTO habitDTO);
        
        Task DeleteHabit(int id, int userId);

        Task CompleteHabitInstance(int id, int userId);

        Task ProcessAllHabitsMissedInstances();
    }
}
