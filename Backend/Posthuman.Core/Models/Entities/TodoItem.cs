using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Represents single "to-do" task
    /// </summary>
    [Table("TodoItems")]
    public class TodoItem
    {
        public TodoItem()
        {
            Title = "";
            Description = "";

            ParentId = null;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }               // Is task completed?
        public bool IsVisible { get; set; }                 // Is visibility on list turned on
        public DateTime? Deadline { get; set; }             // Date until when task should be completed
        public DateTime CreationDate { get; set; }         // Date when task was created
        public DateTime? CompletionDate { get; set; }       // Date when task was marked as 'Completed'

        // Parent & Subtasks
        public int? ParentId { get; set; }
        [JsonIgnore]
        public virtual TodoItem Parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<TodoItem> Subtasks { get; set; }

        // Owner
        public int UserId { get; set; }
        public int AvatarId { get; set; }
        [JsonIgnore]
        public virtual Avatar Avatar { get; set; }
        
        public bool IsTopLevel() =>  ParentId == null;
        public bool HasSubtasks() => Subtasks != null && Subtasks.Count > 0;
        public bool HasUnfinishedSubtasks() => FinishedSubtasksCount() < SubtasksCount();
        public int SubtasksCount()
        {
            if (!HasSubtasks())
                return 0;
            else
            {
                var count = Subtasks.Count;
                foreach (var subtask in Subtasks)
                    count += subtask.SubtasksCount();
                return count;
            }
        }

        public int FinishedSubtasksCount()
        {
            if (!HasSubtasks())
                return 0;
            else
            {
                var count = Subtasks.Where(s => s.IsCompleted).Count();
                foreach(var subtask in Subtasks)
                    count += subtask.FinishedSubtasksCount();
                return count;
            }
        }

        // Indicates how deep in tasks hierarchy this todo item is
        public int NestingLevel()
        {
            int level = 0;
            if (!IsTopLevel())
            {
                var parent = this.Parent;
                while (parent != null)
                {
                    parent = parent.Parent;
                    level++;
                }
            }
            return level;
        }
    }
}
