using Posthuman.Core.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{

    /// <summary>
    /// 
    /// </summary>
    [Table("TechnologyCards")]
    public class TechnologyCard
    {
        public TechnologyCard()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            Body = "";
            Body2 = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
        public string Body2 { get; set; }
        public int RequiredLevel { get; set; }
        public CardCategory Categories { get; set; } 
    }
}
