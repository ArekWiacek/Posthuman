using Posthuman.Core.Models.Entities.Interfaces;
using Posthuman.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    [Table("Habits")]
    public class Habit : IEntity, IOwnable
    {
        public Habit()
        {
            Title = String.Empty;
            Description = String.Empty;
            CreationDate = DateTime.Now;

            RepetitionPeriod = RepetitionPeriod.Weekly;
            DaysOfWeek = 0;
            DayOfMonth = 0;

            CompletedInstances = 0;
            MissedInstances = 0;
            CurrentStreak = 0;
            LongestStreak = 0;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

        // Owner
        public int UserId { get; set; }
        public int AvatarId { get; set; }
        [JsonIgnore]
        public virtual Avatar Avatar { get; set; }

        public RepetitionPeriod RepetitionPeriod { get; set; } // Habit repeats "Daily" or "Weekly" or "Monthly"
        public int DaysOfWeek { get; set; }                    // Days of week when habit recreates for "Weekly" repetition 
        public int DayOfMonth { get; set; }                    // Day of month when habit recreates for "Monthly" repetition

        public DateTime NextIstanceDate { get; set; }
        public DateTime? PreviousInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int CompletedInstances { get; set; }     // How many completed in the past
        public int MissedInstances { get; set; }        // How many not completed in the past
        public int CurrentStreak { get; set; }          // How many completed in a row
        public int LongestStreak { get; set; }          // Longest streak in history of this habit
    }
}
