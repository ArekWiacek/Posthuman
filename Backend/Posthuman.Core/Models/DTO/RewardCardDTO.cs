namespace Posthuman.Core.Models.DTO
{
    public class RewardCardDTO
    {
        public RewardCardDTO()
        {
            ImageUrl = "";
            Title = "";
            Subtitle = "";
            Description = "";
            Description2 = "";
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public int LevelExpected { get; set; }
    }
}
