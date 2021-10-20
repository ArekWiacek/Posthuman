using Posthuman.Core.Models.DTO;

namespace Posthuman.Core.Services
{
    public interface IEventItemsService
    {
        Task<IEnumerable<EventItemDTO>> GetAllEventItems();

        Task<EventItemDTO> GetEventItemById(int id);
    }
}
