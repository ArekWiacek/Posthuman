using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;

namespace Posthuman.Data.Repositories
{
    public class BlogPostsRepository : Repository<BlogPost>, IBlogPostsRepository
    {
        public BlogPostsRepository(PosthumanContext context) : base(context)
        {
        }

        private PosthumanContext BlogPostsDbContext
        {
            get { return Context; }
        }
    }
}
