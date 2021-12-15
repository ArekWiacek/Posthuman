using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Data.Configurations
{
    public class TodoItemCycleConfiguration  : IEntityTypeConfiguration<TodoItemCycle>
    {
        public void Configure(EntityTypeBuilder<TodoItemCycle> builder)
        {
            builder
                .HasKey(ti => ti.Id);

            builder
                .HasOne(tic => tic.TodoItem)
                .WithOne(ti => ti.CycleInfo)
                .HasForeignKey<TodoItemCycle>(tic => tic.TodoItemId)
                .IsRequired(false);

            builder.ToTable("TodoItemCycles");
        }
    }
}
