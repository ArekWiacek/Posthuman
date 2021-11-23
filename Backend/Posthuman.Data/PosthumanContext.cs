using System;
using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Data.Configurations;

namespace Posthuman.Data
{
    public class PosthumanContext : DbContext
    {
        public PosthumanContext(DbContextOptions<PosthumanContext> options) 
            : base(options)
        { 
        }

        public DbSet<Avatar> Avatars { get; set; } = default!;
        public DbSet<TodoItem> TodoItems { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<EventItem> EventItems { get; set; } = default!;
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<RewardCard> RewardCards { get; set; } = default!;

        public static readonly int AvatarId = 2;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());

            modelBuilder.Entity<Avatar>().ToTable("Avatars");
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<EventItem>().ToTable("EventItems");

            modelBuilder.Entity<EventItem>().Property(ei => ei.Type)
                .HasMaxLength(50)
                .HasConversion(ei => ei.ToString(), 
                ei => (EventType)Enum.Parse(typeof(EventType), ei));

            modelBuilder.Entity<EventItem>().Property(ei => ei.RelatedEntityType)
                .HasMaxLength(20)
                .HasConversion(ret => ret.ToString(),
                ret => (EntityType)Enum.Parse(typeof(EntityType), ret));
        }
    }
}
