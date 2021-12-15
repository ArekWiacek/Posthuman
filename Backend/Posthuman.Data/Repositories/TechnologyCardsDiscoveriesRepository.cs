using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class TechnologyCardsDiscoveriesRepository : Repository<TechnologyCardDiscovery>, ITechnologyCardsDiscoveriesRepository
    {
        public TechnologyCardsDiscoveriesRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext AvatarsDbContext
        {
            get { return Context; }
        }
    }
}
