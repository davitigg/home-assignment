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
            builder.Property(d => d.DeviceId).IsRequired().HasMaxLength(255);
            builder.Property(d => d.DevicePinHash).IsRequired().HasMaxLength(255);
            builder.Property(d => d.DeviceBiometricHash).HasMaxLength(255);
            builder.Property(d => d.BiometricEnabled).IsRequired();
            builder.Property(d => d.CreatedAt).IsRequired();

            builder.HasIndex(d => new { d.UserId, d.DeviceId }).IsUnique();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
