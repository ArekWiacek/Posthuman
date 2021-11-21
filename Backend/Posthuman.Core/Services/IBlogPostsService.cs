using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IBlogPostsService
    {
        Task<IEnumerable<BlogPostDTO>> GetBlogPosts();
    }
}
