using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class ProjectsRepository : Repository<Project>, IProjectsRepository
    {
        public ProjectsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext ProjectsDbContext
        {
            get { return Context; }
        }

        public async Task<IEnumerable<Project>> GetAllByAvatarIdAsync(int id)
        {
            return await ProjectsDbContext
                .Projects
                .Where(p => p.AvatarId == id)
                .ToListAsync();
        }
    }
}
