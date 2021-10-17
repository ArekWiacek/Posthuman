using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Represents single "to-do" task
    /// </summary>
    [Table("TodoItems")]
    public class TodoItem
    {
        public TodoItem(
            int id,
            string title,
            string description,
            bool isCompleted,
            DateTime? deadline,
            int? projectId)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.IsCompleted = isCompleted;
            this.Deadline = deadline;
            this.ProjectId = projectId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }             // Date until when task should be completed
        public DateTime? CreationDate { get; set; }         // Date when task was created
        public DateTime? CompletionDate { get; set; }       // Date when task was marked as 'Completed'

        // Parent project - when to-do item is part of something bigger
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
