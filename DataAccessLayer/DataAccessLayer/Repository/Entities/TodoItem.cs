using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Repository.Entities
{
    /// <summary>
    /// Represents single "to-do" task
    /// </summary>
    [Table("TodoItem")]
    public class TodoItem
    {
        public TodoItem(
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
