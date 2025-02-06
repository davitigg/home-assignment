using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class OTPVerification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; } // Foreign Key to Users

        [Required]
        public Guid DeviceId { get; set; } // Foreign Key to Devices

        [Required]
        [MaxLength(6)]
        public string OTP { get; set; } 

        [Required]
        public OTPType Type { get; set; } 

        [Required]
        public string OTPToken { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } 
        public bool IsUsed { get; set; } = false; 

        // Navigation properties
        public User User { get; set; }
        public Device Device { get; set; }
    }

    public enum OTPType
    {
        Mobile,
        Email
    }
}
