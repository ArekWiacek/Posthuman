using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventItemsController : ControllerBase
    {
        private readonly IEventItemsService eventItemsService;

        public EventItemsController(
            IEventItemsService eventItemsService)
        {
            this.eventItemsService = eventItemsService;
        }

        // GET: api/EventItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventItemDTO>>> GetEventItems()
        {
            var eventItems = await eventItemsService.GetAllEventItems();
            return eventItems.ToList();
        }

        // GET: api/EventItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventItemDTO>> GetEventItem(int id)
        {
            var eventItem = await eventItemsService.GetEventItemById(id);

            if (eventItem == null)
                return NotFound();

            return eventItem;
        }
    }
}
