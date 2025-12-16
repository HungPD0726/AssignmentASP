using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public string? IdentityCardFrontSideImageLink { get; set; }
        public string? IdentityCardBackSideImageLink { get; set; }

        public bool Active { get; set; } = true;

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        public ICollection<House> Houses { get; set; }
    }
}