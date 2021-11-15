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

            builder.ToTable("Avatars");
        }
    }
}
