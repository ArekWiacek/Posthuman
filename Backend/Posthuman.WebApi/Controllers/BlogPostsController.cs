using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<BlogPostDTO>>> GetPublishedBlogPosts()
        {
            var blogPosts = await blogPostsService.GetPublishedPostsAsync();
            return Ok(blogPosts);
        }
    }
}
