using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Data.Configurations
{
    public class ProjectConfiguration  : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("Projects");
        }
    }
}
