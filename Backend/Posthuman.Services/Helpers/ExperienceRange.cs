namespace Posthuman.Services.Helpers
{
    public struct ExperienceRange
    {
        public ExperienceRange(int startXp, int endXp)
        {
            StartXp = startXp;
            EndXp = endXp;
        }

        public int StartXp { get; set; }
        public int EndXp { get; set; }
    }
}
