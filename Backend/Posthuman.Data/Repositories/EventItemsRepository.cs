using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class EventItemsRepository : Repository<EventItem>, IEventItemsRepository
    {
        public EventItemsRepository(PosthumanContext context) : base(context)
        {
        }
    }
}
