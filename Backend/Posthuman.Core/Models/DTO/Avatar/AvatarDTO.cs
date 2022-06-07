namespace Posthuman.Core.Models.DTO.Avatar
{
    public class AvatarDTO
    {
        public AvatarDTO()
        {
            Name = "";
            Bio = "";
            CybertribeName = "";
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpToNewLevel { get; set; }
        public int ExpToCurrentLevel { get; set; }

        public string CybertribeName { get; set; }
    }
}
