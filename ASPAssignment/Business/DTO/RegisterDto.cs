using System.ComponentModel.DataAnnotations;

namespace ASPAssignment.Business.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
