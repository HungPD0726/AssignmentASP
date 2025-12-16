using System.ComponentModel.DataAnnotations;

namespace ASPAssignment.Business.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string TypeRole { get; set; }
    }
}
