using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Represents "project" - some bigger "goal" or "quest", containing multiple connected tasks ("to-do items")
    /// </summary>

    [Table("Projects")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }                // Is project finished?

        public DateTime CreationDate { get; set; }          // Date when I created project (automatical)
        public DateTime? StartDate { get; set; }            // Planned date "when to start", if not set then today
        public DateTime? FinishDate { get; set; }           // Date when I completed last to-do item (calculated)

        // Owner
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }

        // Collection of nested todo-items to complete in order to finish the project 
        [JsonIgnore]
        public virtual ICollection<TodoItem> TodoItems { get; set; }

        public int TotalSubtasks { get; set; }
        public int CompletedSubtasks { get; set; }

        public bool IsTopLevel()
        {
            return TodoItems != null && TodoItems.Count() > 0;
        }
    }
}
