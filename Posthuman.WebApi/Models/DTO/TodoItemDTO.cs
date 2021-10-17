namespace PosthumanWebApi.Models.DTO
{
    public class TodoItemDTO
    {
        public TodoItemDTO(
            int id, 
            string title, 
            string description, 
            bool isCompleted, 
            DateTime? deadline,
            int? projectId = null)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.IsCompleted = isCompleted;
            this.Deadline = deadline;
            this.ProjectId = projectId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }
        public int? ProjectId { get; set; }
    }
}
