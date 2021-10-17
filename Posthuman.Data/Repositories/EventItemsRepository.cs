using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Posthuman.Data.Repositories
{
    public class EventItemsRepository : Repository<EventItem>, IEventItemsRepository
    {
        public EventItemsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext EventItemsDbContext
        {
            get { return Context as PosthumanContext; }
        }
    }
}
