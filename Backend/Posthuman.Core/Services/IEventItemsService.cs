using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IEventItemsService
    {
        Task<IEnumerable<EventItemDTO>> GetAllEventItems();

        Task<EventItemDTO> GetEventItemById(int id);
    }
}
