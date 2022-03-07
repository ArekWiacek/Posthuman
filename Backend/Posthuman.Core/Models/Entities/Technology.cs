using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Technology enables part of functionality in application
    /// </summary>
    [Table("Technologies")]
    public class Technology
    {
        public Technology()
        {
            Title = "";
            Description = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TechnologyTag Tag { get; set; }
        public int TechPointsCost { get; set; }
        public int RequiredLevel { get; set; }
        public virtual ICollection<Technology> RequiredTechnologies { get; set; }
    }
}
