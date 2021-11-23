using System;
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
    [Table("RewardCards")]
    public class RewardCard
    {
        public RewardCard()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            Description = "";
            Description2 = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public int LevelExpected { get; set; }
    }
}
