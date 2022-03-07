using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class TechnologyCardsRepository : Repository<TechnologyCard>, ITechnologyCardsRepository
    {
        public TechnologyCardsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext AvatarsDbContext
        {
            get { return Context; }
        }
    }
}
