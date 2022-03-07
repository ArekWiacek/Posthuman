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
                .HasKey(a => a.Id);

            //builder
            //    .HasOne(a => a.User)
            //    .WithOne(u => u.Avatar)
            //    .HasForeignKey<Avatar>(a => a.UserId);

            builder.ToTable("Avatars");
        }
    }
}
