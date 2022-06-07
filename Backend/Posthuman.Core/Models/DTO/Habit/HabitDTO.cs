using System;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Core.Models.DTO.Habit
{
    public class HabitDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int RepetitionPeriod { get; set; }  
        public string[] DaysOfWeek { get; set; }                      
        public int? DayOfMonth { get; set; }                    

        public DateTime NextIstanceDate { get; set; }
        public DateTime? PreviousIstanceDate { get; set; }
        public DateTime? LastInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int CompletedInstances { get; set; }     
        public int MissedInstances { get; set; }       
        public int CurrentStreak { get; set; }      
        public int LongestStreak { get; set; }     
    }
}
