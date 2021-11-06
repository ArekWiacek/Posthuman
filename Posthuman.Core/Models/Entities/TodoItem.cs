﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        // Parent TodoItem
        public int? ParentId { get; set; }
        [JsonIgnore]
        public virtual TodoItem Parent { get; set; }

        //[JsonIgnore]

        //ReferenceHandler.Preserve
        [JsonIgnore]            
        public virtual ICollection<TodoItem> Subtasks { get; set; }

        // Avatar (Think as user "hero" - with level, exp and so on)
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }


        // Parent project - when to-do item is part of something bigger
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public bool IsTopLevel()
        {
            return ParentId == null;
        }

        public bool HasSubtasks()
        {
            return Subtasks != null && Subtasks.Count > 0;
        }

        public int SubtasksCount()
        {
            if (!HasSubtasks())
                return 0;
            else
            {
                int count = 0;

                count += Subtasks.Count;

                //foreach(var subtask in Subtasks)
                //{
                //    count += subtask.SubtasksCount();
                //}

                return count;
            }
        }

        public int FinishedSubtasksCount()
        {
            if (!HasSubtasks())
                return 0;
            else
            {
                int count = 0;

                count += Subtasks.Where(s => s.IsCompleted).ToList().Count;

                //foreach(var subtask in Subtasks)
                //{
                //    count += subtask.SubtasksCount();
                //}

                return count;
            }
        }

        public int NestingLevel()
        {
            int level = 0;

            if (!IsTopLevel())
            {
                var parent = this.Parent;

                while (parent != null)
                {
                    parent = this.Parent;
                    level++;
                }
            }

            return level;
        }
    }
}
