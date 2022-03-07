using Posthuman.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    [Table("TodoItemCycles")]
    public class TodoItemCycle
    {
        public TodoItemCycle()
        {
            TodoItem = null;
            StartDate = DateTime.Now.Date;
            EndDate = null;
            TotalInstances = 0;
            CompletedInstances = 0;
            MissedInstances = 0;
            InstancesToComplete = 0;
            InstancesStreak = 0;
        }

        public int Id { get; set; }
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }
        public bool IsInfinite { get; set; }

        public RepetitionPeriod RepetitionPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime NextIstanceDate { get; set; }
        public DateTime? LastInstanceDate { get; set; }
        public DateTime? LastCompletedInstanceDate { get; set; }

        public int TotalInstances { get; set; }         // number of all occurencies of cycle from startDate till endDate
        public int CompletedInstances { get; set; }     // How many completed in the past
        public int MissedInstances { get; set; }        // How many not completed in the past
        public int InstancesToComplete { get; set; }    // How many left till endDate
        public int InstancesStreak { get; set; }        // How many completed in a row
    }
}
