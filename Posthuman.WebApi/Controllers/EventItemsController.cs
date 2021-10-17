using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models;
using PosthumanWebApi.Models.Entities;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventItemsController : ControllerBase
    {
        private readonly PosthumanContext _context;
        public EventItemsController(PosthumanContext context)
        {
            _context = context;
        }

        // GET: api/EventItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventItem>>> GetEventItems()
        {
            var eventItems = _context.EventItems.ToList();

            return await _context
                .EventItems
                .ToListAsync();
        }

        // GET: api/EventItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventItem>> GetEventItem(int id)
        {
            var eventItem = await _context.EventItems.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return eventItem;
        }
    }
}
