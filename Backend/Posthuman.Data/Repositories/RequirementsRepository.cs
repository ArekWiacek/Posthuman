using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class RequirementsRepository : Repository<Requirement>, IRequirementsRepository
    {
        public RequirementsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext RequirementsDbContext
        {
            get { return Context; }
        }
    }
}
