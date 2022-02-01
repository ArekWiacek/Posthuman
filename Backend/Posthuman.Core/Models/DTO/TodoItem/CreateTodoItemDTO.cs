using System;

namespace Posthuman.Core.Models.DTO
{
    public class CreateTodoItemDTO
    {
        public CreateTodoItemDTO()
        {
            Title = "";
            Description = "";
           
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        // public int AvatarId { get; set; }
        public int? ParentId { get; set; }
        public int? ProjectId { get; set; }
        public bool IsCyclic { get; set; }
        public int? RepetitionPeriod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
