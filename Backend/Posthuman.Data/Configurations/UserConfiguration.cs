using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Entities.Authentication;

namespace Posthuman.Data.Configurations
{
    public class UserConfiguration  : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .HasOne(u => u.Avatar)
                .WithOne(a => a.User)
                .HasForeignKey<Avatar>(a => a.UserId)
                .IsRequired(false);

            builder
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                //.HasForeignKey<Role>(r => r.)
                .IsRequired(false);

            //builder.ToTable("Avatars");
        }
    }
}
