using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Repository.Entities;

namespace DataAccessLayer.Models
{
    public class PosthumanContext : DbContext
    {
        public PosthumanContext(DbContextOptions<PosthumanContext> options) 
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = default!;
    }
}
