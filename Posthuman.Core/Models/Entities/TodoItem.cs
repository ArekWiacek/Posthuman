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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }               // Is task completed?
        public DateTime? Deadline { get; set; }             // Date until when task should be completed
        public DateTime? CreationDate { get; set; }         // Date when task was created
        public DateTime? CompletionDate { get; set; }       // Date when task was marked as 'Completed'

        // Owner
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }


        // Parent project - when to-do item is part of something bigger
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
