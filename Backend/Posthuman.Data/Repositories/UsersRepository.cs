using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext UsersDbContext
        {
            get { return Context; }
        }

        public async Task<User> GetByEmail(string email)
        {
            return await UsersDbContext
                .Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
