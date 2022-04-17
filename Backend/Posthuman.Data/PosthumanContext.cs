using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Entities.Authentication;
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

        // Authentication-related models
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;

        // Game-related models
        public DbSet<Avatar> Avatars { get; set; } = default!;
        public DbSet<TodoItem> TodoItems { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<EventItem> EventItems { get; set; } = default!;
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
        public DbSet<TechnologyCard> TechnologyCards { get; set; } = default!;
        public DbSet<TechnologyCardDiscovery> TechnologyCardsDiscoveries { get; set; } = default!;
        public DbSet<Requirement> Requirements { get; set; } = default!;
        public DbSet<Habit> Habits { get; set; } = default!;

        public static readonly int AvatarId = 2;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyModelConfigurations(modelBuilder);
            ConfigureEntities(modelBuilder);
            ConfigureEnums(modelBuilder);

            // TEMP
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();

            CreateInitialData(modelBuilder);
        }

        private void CreateInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "RegisteredUser" }
            );
        }

        private void ApplyModelConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avatar>().ToTable("Avatars");
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<EventItem>().ToTable("EventItems");
            modelBuilder.Entity<BlogPost>().ToTable("BlogPosts");
            modelBuilder.Entity<TechnologyCard>().ToTable("TechnologyCards");
            modelBuilder.Entity<TechnologyCardDiscovery>().ToTable("TechnologyCardsDiscoveries");
            modelBuilder.Entity<Requirement>().ToTable("Requirements");
            modelBuilder.Entity<Habit>().ToTable("Habits");

            EnumToNumberConverter<CardCategory, int> converter = new EnumToNumberConverter<CardCategory, int>();
            modelBuilder.Entity<TechnologyCard>()
                        .Property(e => e.Categories)
                        .HasConversion(converter);
        }

        private void ConfigureEnums(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventItem>().Property(ei => ei.Type)
                .HasMaxLength(50)
                .HasConversion(
                    t => t.ToString(),
                    t => (EventType)Enum.Parse(typeof(EventType), t));

            modelBuilder.Entity<EventItem>().Property(ei => ei.RelatedEntityType)
                .HasMaxLength(20)
                .HasConversion(
                    ret => ret.ToString(),
                    ret => (EntityType)Enum.Parse(typeof(EntityType), ret));

            modelBuilder.Entity<Habit>().Property(tic => tic.RepetitionPeriod)
                .HasMaxLength(20)
                .HasConversion(
                    rp => rp.ToString(),
                    rp => (RepetitionPeriod)Enum.Parse(typeof(RepetitionPeriod), rp));

            modelBuilder.Entity<Requirement>().Property(r => r.Type)
                .HasMaxLength(20)
                .HasConversion(
                    r => r.ToString(),
                    r => (RequirementType)Enum.Parse(typeof(RequirementType), r));
        }
    }
}
