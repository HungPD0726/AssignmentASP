using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        [Key]
        public int TokenID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameRole { get; set; }

        [Required]
        public int RequestNameRoleId { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;
    }
}
