using Posthuman.Core.Models.Enums;
using System;

namespace Posthuman.Core.Models.DTO
{
    public class EventItemDTO
    {
        public int Id { get; set; }
        public int AvatarId { get; set; }               
        public EventType Type { get; set; }             
        public DateTime Occured { get; set; }           
        public EntityType? RelatedEntityType { get; set; } 
        public int? RelatedEntityId { get; set; }
        public int ExpGained { get; set; }              
    }
}
