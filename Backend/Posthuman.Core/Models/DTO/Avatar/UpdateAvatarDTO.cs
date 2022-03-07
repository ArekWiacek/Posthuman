namespace Posthuman.Core.Models.DTO.Avatar
{
    public class UpdateAvatarDTO
    {
        public UpdateAvatarDTO()
        {
            Name = Bio = CybertribeName = "";
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string CybertribeName { get; set; }
    }
}
