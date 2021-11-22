using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;

namespace Posthuman.Services
{
    public class BlogPostsService : IBlogPostsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BlogPostsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BlogPostDTO>> GetBlogPosts()
        {
            var blogPosts = await unitOfWork.BlogPosts.GetAllAsync();
            blogPosts = blogPosts.OrderByDescending(bp => bp.PublishDate);
            var blogPostsMapped = mapper.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostDTO>>(blogPosts);
            return blogPostsMapped;
        }
    }
}
