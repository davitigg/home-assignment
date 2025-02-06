using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.UserId).IsRequired();
            builder.Property(d => d.DeviceId).IsRequired().HasMaxLength(100);
            builder.HasIndex(d => d.DeviceId).IsUnique(); // Ensures unique devices
            builder.Property(d => d.DeviceAuthHash).IsRequired().HasMaxLength(256);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
