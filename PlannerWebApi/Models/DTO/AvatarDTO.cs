namespace PosthumanWebApi.Models.DTO
{
    public class AvatarDTO
    {
        public AvatarDTO(
            int id, 
            string name, 
            string bio, 
            int level, 
            int exp)
        {
            this.Id = id;
            this.Name = name;
            this.Bio = bio;
            this.Level = level;
            this.Exp = exp;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int ProjectsCount { get; set; }
        public int TasksCount { get; set; }

        public int DaysAlive { get; set; }
    }
}
