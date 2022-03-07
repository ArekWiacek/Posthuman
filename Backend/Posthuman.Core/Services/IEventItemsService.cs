using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IEventItemsService
    {
        Task<IEnumerable<EventItemDTO>> GetAllEventItems();

        Task<EventItemDTO> GetEventItemById(int id);

        Task<EventItem> CreateEventItem(int userId, EventType eventType, EntityType? relatedEntityType, int? relatedEntityId, int expGained = 0);
    }
}
