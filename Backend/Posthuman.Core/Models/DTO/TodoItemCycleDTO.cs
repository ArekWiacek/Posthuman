using System;

namespace Posthuman.Core.Models.DTO
{
    public class TodoItemCycleDTO
    {
        public TodoItemCycleDTO()
        {
        }

        public int RepetitionPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsInfinite { get; set; }
    }
}
