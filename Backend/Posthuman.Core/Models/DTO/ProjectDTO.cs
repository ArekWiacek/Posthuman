using System;

namespace Posthuman.Core.Models.DTO
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
            Title = "";
            Description = "";
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }

        public DateTime CreationDate { get; set; }          
        public DateTime? StartDate { get; set; }            
        public DateTime? FinishDate { get; set; }          

        public int AvatarId { get; set; }

        public int TotalSubtasks { get; set; }
        public int CompletedSubtasks { get; set; }
    }
}
