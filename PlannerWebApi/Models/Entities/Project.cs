using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosthumanWebApi.Models.Entities
{
    /// <summary>
    /// Represents "project" - some bigger "goal" or "quest", containing multiple connected tasks ("to-do items")
    /// </summary>

    [Table("Project")]
    public class Project
    {
        public Project(
            long id,
            string title,
            string description,
            bool isFinished,
            DateTime? startDate)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.IsFinished = isFinished;
            this.StartDate = startDate;
    }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }

        public DateTime CreationDate { get; set; }          // Date when I created project              (automatical)
        public DateTime? StartDate { get; set; }            // Planned date "when to start", if not set then today
        public DateTime? FinishDate { get; set; }           // Date when I completed last to-do item (calculated)


        public ICollection<TodoItem> TodoItems { get; set; }    // Collection of nested todo-items to complete in order to finish the project 
    }
}
