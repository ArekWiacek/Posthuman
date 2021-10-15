using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosthumanWebApi.Models.Entities
{
    /// <summary>
    /// Represents single "event" - something that happened in app, e.g. user created project or completed task
    /// Used mostly for gamification mechanism, statistical / historical and logging purposes
    /// </summary>
    [Table("EventItems")]
    public class EventItem
    {
        public EventItem(
            int id,
            int avatarId,
            int type, 
            DateTime occured)
        {
            this.Id = id;
            this.AvatarId = avatarId;
            this.Type = type;
            this.Occured = occured;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AvatarId { get; set; }                // Owner
        public int Type { get; set; }
        public DateTime Occured { get; set; }

    }
}
