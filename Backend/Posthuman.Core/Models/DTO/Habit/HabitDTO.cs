using System;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Core.Models.DTO.Habit
{
    public class HabitDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 

        public RepetitionPeriod RepetitionPeriod { get; set; }  
        public int? WeekDays { get; set; }                      
        public int? DayOfMonth { get; set; }                    

        public DateTime NextIstanceDate { get; set; }
        public DateTime? LastInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int CompletedInstances { get; set; }     
        public int MissedInstances { get; set; }       
        public int InstancesStreak { get; set; }      
        public int MaxInstancesStreak { get; set; }     
    }
}
