using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models.Entities;
using PosthumanWebApi.Models.Enums;
using System;

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
                .HasConversion(ei => ei.ToString(), 
                ei => (EventType)Enum.Parse(typeof(EventType), ei));

            modelBuilder.Entity<EventItem>().Property(ei => ei.RelatedEntityType)
                .HasMaxLength(20)
                .HasConversion(ret => ret.ToString(),
                ret => (EntityType)Enum.Parse(typeof(EntityType), ret));
        }
    }
}
