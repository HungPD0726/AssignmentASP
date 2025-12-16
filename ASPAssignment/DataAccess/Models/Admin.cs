using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [StringLength(100)]
        public string AdminName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
