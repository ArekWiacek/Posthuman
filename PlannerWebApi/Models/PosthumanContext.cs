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
        public DbSet<EventItem> EventItems { get; set; } = default!;

        public static readonly int AvatarId = 2;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avatar>().ToTable("Avatars");
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<EventItem>().ToTable("EventItems");

            modelBuilder.Entity<EventItem>().Property(ei => ei.Type)
            .HasMaxLength(50)
            .HasConversion(x => x.ToString(), // to converter
                x => (EventType)Enum.Parse(typeof(EventType), x));// from converter
        }
    }
}
