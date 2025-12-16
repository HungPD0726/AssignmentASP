using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("UserRoles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } // Ví dụ: Student, Landlord

        // Quan hệ: 1 Role có nhiều User
        public ICollection<User> Users { get; set; }
    }
}