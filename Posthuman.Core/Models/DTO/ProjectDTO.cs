namespace Posthuman.Core.Models.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }

        public DateTime CreationDate { get; set; }          // Date when I created project              (automatical)
        public DateTime? StartDate { get; set; }            // Planned date "when to start", if not set then today
        public DateTime? FinishDate { get; set; }           // Date when I completed last to-do item (calculated)

        public int AvatarId { get; set; }

        public int TotalSubtasks { get; set; }
        public int CompletedSubtasks { get; set; }
    }
}
