using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            get { return Context; }
        }

        public async Task<Avatar> GetAvatarForUserAsync(int userId)
        {
            var avatar = await
                AvatarsDbContext
                .Avatars
                .Where(a => a.UserId == userId)
                .FirstOrDefaultAsync();

            if (avatar == null)
                throw new ArgumentException($"Avatar for user with userId: {userId} was not found.", "userId");

            return avatar;
        }
    }
}
