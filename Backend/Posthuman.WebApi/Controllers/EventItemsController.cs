using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventItemsController : ControllerBase
    {
        private readonly ILogger<EventItemsController> logger;
        private readonly IEventItemsService eventItemsService;

        public EventItemsController(
            ILogger<EventItemsController> logger,
            IEventItemsService eventItemsService)
        {
            this.logger = logger;
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
