using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostsService blogPostsService;

        public BlogPostsController(IBlogPostsService blogPostsService)
        {
            this.blogPostsService = blogPostsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDTO>>> GetAvatars()
        {
            var blogPosts = await blogPostsService.GetBlogPosts();
            return Ok(blogPosts);
        }
    }
}
