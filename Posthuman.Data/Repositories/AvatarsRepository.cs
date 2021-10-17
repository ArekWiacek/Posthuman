using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class AvatarsRepository : Repository<Avatar>, IAvatarsRepository
    {
        public AvatarsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext AvatarsDbContext
        {
            get { return Context as PosthumanContext; }
        }
    }
}
