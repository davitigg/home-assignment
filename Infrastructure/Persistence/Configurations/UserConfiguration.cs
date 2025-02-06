using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.ICNumber).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.ICNumber).IsUnique();
            builder.Property(u => u.Mobile).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        }
    }
}

