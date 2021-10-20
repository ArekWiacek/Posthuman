﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Data.Configurations
{
    public class TodoItemConfiguration  : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasKey(ti => ti.Id);

            builder
                .Property(ti => ti.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(ti => ti.Project)
                .WithMany(p => p.TodoItems)
                .HasForeignKey(ti => ti.ProjectId)
                .IsRequired(false);

            builder
                .HasOne<Avatar>(todoItem => todoItem.Avatar)
                .WithMany(avatar => avatar.TodoItems);
                
            builder.ToTable("TodoItems");
        }
    }
}
