using Posthuman.Core.Models.Enums;

namespace Posthuman.Core.Models.DTO
{
    public class RequirementDTO
    {   public int Id { get; set; }
        public RequirementType Type { get; set; }
        public int? Level { get; set; }
        public int? Exp { get; set; }
    }
}
