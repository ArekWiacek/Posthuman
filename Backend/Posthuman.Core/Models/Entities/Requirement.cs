using Posthuman.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    [Table("Requirements")]
    public class Requirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RequirementType Type { get; set; }
        public int? Level { get; set; }
        public int? Exp { get; set; }
    }
}
