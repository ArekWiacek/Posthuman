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
            TodoItem = new TodoItem();

            Instances = 0;
            CompletedInstances = 0;
            MissedInstances = 0;
            InstancesStreak = 0;
        }

        public int Id { get; set; }
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }

        public RepetitionPeriod RepetitionPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsInfinite { get; set; }

        // If end date provider, then Instances holds number of all occurencies of task
        // Respectively CompletedInstances and MissedInstances as well
        // InstancesStreak holds completed instances in a row
        public int Instances { get; set; }
        public int CompletedInstances { get; set; }
        public int MissedInstances { get; set; }
        public int InstancesStreak { get; set; }
    }
}
