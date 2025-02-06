using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Primary Key (Unique per registration)

        [Required]
        public Guid UserId { get; set; } // Foreign Key to Users

        [Required]
        public string DeviceId { get; set; } // Same DeviceId can exist for multiple users

        public string? DeviceAuthHash { get; set; } // Stores hashed(DeviceId + PIN)

        public bool MobileVerified { get; set; } = false; // ✅ Each user must verify separately
        public bool EmailVerified { get; set; } = false; // ✅ Each user must verify separately
        public bool IsActive { get; set; } = false; // ✅ Only active devices can log in

        // Navigation property
        public User User { get; set; }
    }
}
