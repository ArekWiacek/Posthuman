using Posthuman.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    [Table("Habits")]
    public class Habit
    {
        public Habit()
        {
            CompletedInstances = 0;
            MissedInstances = 0;
            InstancesToComplete = 0;
            InstancesStreak = 0;
            RepetitionPeriod = RepetitionPeriod.Daily;
        }

        public RepetitionPeriod RepetitionPeriod { get; set; }  // Habit repeats "Daily" or "Weekly" or "Monthly"
        public int? WeekDays { get; set; }                      // Days of week when habit recreates for "Weekly" repetition 
        public int? DayOfMonth { get; set; }                    // Day of month when habit recreates for "Monthly" repetition

        public DateTime NextIstanceDate { get; set; }
        public DateTime? LastInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int CompletedInstances { get; set; }     // How many completed in the past
        public int MissedInstances { get; set; }        // How many not completed in the past
        public int InstancesToComplete { get; set; }    // How many left till endDate
        public int InstancesStreak { get; set; }        // How many completed in a row
    }
}
