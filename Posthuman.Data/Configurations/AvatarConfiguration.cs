using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Data.Configurations
{
    public class AvatarConfiguration  : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder
                .HasKey(p => p.Id);

            

            //builder
            ///    .HasOne(ti => ti.Project)
            //    .WithMany(p => p.TodoItems)
            //    .HasForeignKey(ti => ti.ProjectId);

            builder.ToTable("Projects");
                
               // .UseIdentityColumn();
        }
    }
}
