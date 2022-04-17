using Posthuman.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    [Table("Habits")]
    public class Habit
    {
        public Habit()
        {
            Title = String.Empty;
            Description = String.Empty;
            CreationDate = DateTime.Now;

            RepetitionPeriod = RepetitionPeriod.Weekly;
            WeekDays = null;
            DayOfMonth = null;

            CompletedInstances = 0;
            MissedInstances = 0;
            InstancesStreak = 0;
            MaxInstancesStreak = 0;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime CreationDate { get; set; }

        // Owner
        public int UserId { get; set; }
        public int AvatarId { get; set; }
        [JsonIgnore]
        public virtual Avatar Avatar { get; set; }

        public RepetitionPeriod RepetitionPeriod { get; set; }  // Habit repeats "Daily" or "Weekly" or "Monthly"
        public int? WeekDays { get; set; }                      // Days of week when habit recreates for "Weekly" repetition 
        public int? DayOfMonth { get; set; }                    // Day of month when habit recreates for "Monthly" repetition

        public DateTime NextIstanceDate { get; set; }
        public DateTime? LastInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int CompletedInstances { get; set; }     // How many completed in the past
        public int MissedInstances { get; set; }        // How many not completed in the past
        public int InstancesStreak { get; set; }        // How many completed in a row
        public int MaxInstancesStreak { get; set; }     // Longest streak in history of this habit
    }
}
