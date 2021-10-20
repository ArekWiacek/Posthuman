using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;

namespace Posthuman.Services
{
    public class EventItemsService : IEventItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EventItemsService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<EventItemDTO>> GetAllEventItems()
        {
            var allEvents = await unitOfWork.EventItems.GetAllAsync();

            var allMapped =
                mapper.Map<IEnumerable<EventItemDTO>>(allEvents);

            return allMapped.ToList();
        }

        public async Task<EventItemDTO> GetEventItemById(int id)
        {
            var eventItem = await unitOfWork.EventItems.GetByIdAsync(id);

            if (eventItem != null)
                return mapper.Map<EventItemDTO>(eventItem);

            return null;
        }
    }
}
