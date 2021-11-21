using System;

namespace Posthuman.Core.Models.DTO
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public BlogPostDTO()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            AdditionalText = "";
            Content = "";
            Author = "";
        }

        public string Author { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string AdditionalText { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
