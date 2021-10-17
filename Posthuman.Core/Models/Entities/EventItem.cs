using Posthuman.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// Represents single "event" - something that happened in app, e.g. user created project or completed task
    /// Used mostly for gamification mechanism, statistical / historical and logging purposes
    /// </summary>
    [Table("EventItems")]
    public class EventItem
    {
        public EventItem(
            int avatarId,
            EventType type,
            DateTime occured,
            EntityType? relatedEntityType = null,
            int? relatedEntityId = null)
        {
            this.AvatarId = avatarId;
            this.Type = type;
            this.Occured = occured;

            this.RelatedEntityType = relatedEntityType;
            this.RelatedEntityId = relatedEntityId;

            this.ExpGained = ExpReward[type];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AvatarId { get; set; }               // Owner
        public EventType Type { get; set; }             // Type of event, "what happened"
        public DateTime Occured { get; set; }           // When happened


        // Type of entity related to this EventItem
        public EntityType? RelatedEntityType { get; set; }
        
        // ID of related entity
        public int? RelatedEntityId { get; set; }
        
        public int ExpGained { get; set; }              // How much XP user gained from this action

        // Exp points rewards for different events
        private Dictionary<EventType, int> ExpReward = new Dictionary<EventType, int>()
        {
            { EventType.None, 0 },

            { EventType.TodoItemCreated, 10 },
            { EventType.TodoItemDeleted, -10 },
            { EventType.TodoItemModified, 0 },
            { EventType.TodoItemCompleted, 30 },

            { EventType.ProjectCreated, 30 },
            { EventType.ProjectDeleted, -30 },
            { EventType.ProjectModified, 0 },
            { EventType.ProjectFinished, 100 }
        };
    }
}
