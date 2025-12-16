using System.ComponentModel.DataAnnotations;

namespace ASPAssignment.Business.DTO
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string PhoneNumber { get; set; }

        // Mặc định đăng ký sẽ là Student (RoleID = 2 chẳng hạn)
        // Nếu muốn cho đăng ký Landlord thì có thể truyền thêm
        public string RoleName { get; set; } = "Student";
    }
}