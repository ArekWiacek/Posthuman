using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<BlogPost>> GetPublishedPostsAsync()
        {
            var publishedPosts = await
                BlogPostsDbContext
                .BlogPosts
                .Where(bp => bp.IsPublished)
                .ToListAsync();

            if (publishedPosts == null)
                throw new Exception("No published blog posts were found");

            return publishedPosts;
        }
    }
}
