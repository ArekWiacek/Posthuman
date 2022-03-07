using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Posthuman.Core.Models.Enums;

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
            int userId,
            EventType type,
            DateTime occured,
            EntityType? relatedEntityType = null,
            int? relatedEntityId = null,
            int expGained = 0)
        {
            UserId = userId;
            Type = type;
            Occured = occured;

            RelatedEntityType = relatedEntityType;
            RelatedEntityId = relatedEntityId;
            ExpGained = expGained;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public EventType Type { get; set; }             // Type of event, "what happened"
        public DateTime Occured { get; set; }           // When happened


        // Type of entity related to this EventItem
        public EntityType? RelatedEntityType { get; set; }
        
        // ID of related entity
        public int? RelatedEntityId { get; set; }
        
        public int ExpGained { get; set; }              // How much XP user gained from this action
    }
}
