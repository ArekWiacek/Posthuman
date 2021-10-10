namespace PosthumanWebApi.Models.DTO
{
    public class TodoItemDTO
    {
        public TodoItemDTO(
            long id, 
            string title, 
            string description, 
            bool isCompleted, 
            DateTime? deadline)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.IsCompleted = isCompleted;
            this.Deadline = deadline;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
