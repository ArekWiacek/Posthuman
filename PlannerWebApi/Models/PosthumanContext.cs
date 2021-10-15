using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models.Entities;

namespace PosthumanWebApi.Models
{
    public class PosthumanContext : DbContext
    {
        public PosthumanContext(DbContextOptions<PosthumanContext> options) 
            : base(options)
        { 
            // this.Database.Log = (lg) => WriteLine(s);
        }
        public DbSet<Avatar> Avatars { get; set; } = default!;
        public DbSet<TodoItem> TodoItems { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avatar>().ToTable("Avatars");
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<Project>().ToTable("Projects");
        }
    }
}
