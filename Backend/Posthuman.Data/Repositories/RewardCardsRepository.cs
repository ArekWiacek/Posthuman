using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class RewardCardsRepository : Repository<RewardCard>, IRewardCardsRepository
    {
        public RewardCardsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext AvatarsDbContext
        {
            get { return Context; }
        }
    }
}
