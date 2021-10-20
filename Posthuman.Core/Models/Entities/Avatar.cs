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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }            // User nickname
        public string Bio { get; set; }             // "About" user
        public DateTime CreationDate { get; set; }  // Date when user created account        
        
        public int Level { get; set; }              // Avatar level - it grows when gaining experience
        public int Exp { get; set; }                // Experience points


        // Temporary value to indicate whether this entity is "current user"
        // This is used to quickly switch between avatars during development
        public bool IsActive { get; set; }          // Is this current user?


        // Owned Projects
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        // Owned Todo Items (tasks)
        [JsonIgnore]
        public virtual ICollection<TodoItem> TodoItems { get; set; }

        
    }
}
