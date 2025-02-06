using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ICNumber { get; set; } // Unique Identifier

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Email { get; set; }

        // Navigation properties
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }

}
