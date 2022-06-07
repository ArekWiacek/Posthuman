using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;
using System;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;

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

            if (eventItem == null)
                throw new ArgumentException($"Could not obtain EventItem of ID: {id}.", "id");

            return mapper.Map<EventItemDTO>(eventItem);
        }


        public async Task<EventItem> AddNewEventItem(
            int userId,
            EventType eventType,
            EntityType? relatedEntityType,
            int? relatedEntityId,
            int expGained = 0)
        {
            var eventItem = new EventItem(
                userId,
                eventType,
                DateTime.Now,
                relatedEntityType,
                relatedEntityId,
                expGained);

            await unitOfWork.EventItems.AddAsync(eventItem);

            return eventItem;
        }
    }
}
