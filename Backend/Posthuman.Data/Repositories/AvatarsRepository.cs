﻿using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Avatar> GetActiveAvatarAsync()
        {
            var avatar = await 
                AvatarsDbContext
                .Avatars
                .Where(a => a.IsActive == true)
                .FirstOrDefaultAsync();

            if (avatar == null)
                throw new Exception();

            return avatar;
        }
    }
}