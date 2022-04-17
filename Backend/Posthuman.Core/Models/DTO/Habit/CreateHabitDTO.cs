using Posthuman.Core.Models.Enums;

namespace Posthuman.Core.Models.DTO.Habit
{
    public class CreateHabitDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public RepetitionPeriod RepetitionPeriod { get; set; }  
        public int? WeekDays { get; set; }                      
        public int? DayOfMonth { get; set; }                    

    }
}
