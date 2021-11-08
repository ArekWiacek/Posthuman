using Posthuman.Core.Models.Enums;
using System;

namespace Posthuman.Core.Models.DTO
{
    public class EventItemDTO
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }               // Owner
        public EventType Type { get; set; }             // Type of event, "what happened"
        public DateTime Occured { get; set; }           // When happened
        public EntityType? RelatedEntityType { get; set; }

        public int? RelatedEntityId { get; set; }
        public int ExpGained { get; set; }              // How much XP user gained from this action
    }
}
