namespace Posthuman.Core.Models.DTO.Habit
{
    public class CreateHabitDTO
    {
        public CreateHabitDTO()
        {
            Title = string.Empty;
            Description = string.Empty;
            RepetitionPeriod = string.Empty;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string RepetitionPeriod { get; set; }  
        public string[] DaysOfWeek { get; set; }                      
        public int DayOfMonth { get; set; }                    

    }
}
