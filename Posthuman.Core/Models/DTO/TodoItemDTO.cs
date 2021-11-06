namespace Posthuman.Core.Models.DTO
{
    public class TodoItemDTO
    {
        public TodoItemDTO()
        {
            Title = "";
            Description = "";
            // Subtasks = new List<TodoItemDTO>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }

        public int AvatarId { get; set; }
        public int? ProjectId { get; set; }

        public bool IsTopLevel { get; set; }
        public bool HasSubtasks { get; set; }
        public int NestingLevel { get; set; }
        public int SubtasksCount { get; set; }
        public int FinishedSubtasksCount { get; set; }

        // Parent TodoItem
        public int? ParentId { get; set; }
        //public virtual ICollection<TodoItemDTO> Subtasks { get; set; }
    }
}
