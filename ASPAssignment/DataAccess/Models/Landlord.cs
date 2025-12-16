using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("Landlords")]
    public class Landlord
    {
        [Key]
        public int LandlordID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(50)]
        public string VerificationStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<House> Houses { get; set; }
    }
}
