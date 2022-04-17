using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Data.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasKey(ti => ti.Id);

            builder
                .Property(ti => ti.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .HasOne(ti => ti.Project)
                .WithMany(p => p.TodoItems)
                .HasForeignKey(ti => ti.ProjectId)
                .IsRequired(false);

            //builder
            //    .HasOne(todoItem => todoItem.Avatar)
            //    .WithMany(avatar => avatar.TodoItems);

            builder
                .HasOne(ti => ti.Parent)
                .WithMany(p => p.Subtasks)
                .HasForeignKey(ti => ti.ParentId)
                .IsRequired(false);

            builder.ToTable("TodoItems");
        }
    }
}
