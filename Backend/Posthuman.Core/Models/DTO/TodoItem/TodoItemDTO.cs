using System;

namespace Posthuman.Core.Models.DTO
{
    public class TodoItemDTO
    {
        public TodoItemDTO()
        {
            Title = "";
            Description = "";
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? Deadline { get; set; }

        //public int UserId { get; set; }
        public int AvatarId { get; set; }
        public int? ParentId { get; set; }
        public int? ProjectId { get; set; }

        public bool IsTopLevel { get; set; }
        public int NestingLevel { get; set; }
        public bool HasSubtasks { get; set; }
        public bool HasUnfinishedSubtasks { get; set; }
        public int SubtasksCount { get; set; }
        public int FinishedSubtasksCount { get; set; }    
    }
}
