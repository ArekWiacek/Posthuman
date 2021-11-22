using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public BlogPost()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            AdditionalText = "";
            Content = "";
            Author = "";
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
        public string AdditionalText { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }  
        public DateTime PublishDate { get; set; }
    }
}
