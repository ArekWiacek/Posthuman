using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Represents game player character (hero)
    /// </summary>
    [Table("Avatars")]
    public class Avatar
    {
        public Avatar(
            int id,
            string name,
            string bio,
            DateTime creationDate, 
            int level = 0,
            int exp = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Bio = bio;
            this.CreationDate = creationDate;
            this.Level = level;
            this.Exp = exp;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime CreationDate { get; set; }      // Date when user registered account        
        
        public int Level { get; set; }                  // Avatar level - it grows when gaining experience
        public int Exp { get; set; }                    // Experience points

        // Owned Projects
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
