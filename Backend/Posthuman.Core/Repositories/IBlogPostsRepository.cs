using Posthuman.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    public interface IBlogPostsRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetPublishedPostsAsync();
    }
}