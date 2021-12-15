namespace Posthuman.Core.Models.DTO
{
    public class TechnologyCardDTO
    {
        public TechnologyCardDTO()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            Body = "";
            Body2 = "";
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
        public string Body2 { get; set; }
        public int RequiredLevel { get; set; }
        public int Categories { get; set; } 
    }
}
