using System.Collections.Generic;
using System.Threading.Tasks;
using Posthuman.Core.Models.DTO.Habit;

namespace Posthuman.Core.Services
{
    public interface IHabitsService
    {
        Task<IEnumerable<HabitDTO>> GetAllHabits(int userId);

        Task<HabitDTO> CreateHabit(int userId, CreateHabitDTO habitDTO);
        
        Task UpdateHabit(CreateHabitDTO habitDTO);
        
        Task DeleteHabit(int id);

        Task CompleteHabitInstance(HabitDTO habitDTO);
    }
}
