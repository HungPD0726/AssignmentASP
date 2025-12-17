using System.ComponentModel.DataAnnotations;

namespace ASPAssignment.Business.DTO
{
    public class UpdateProfileModel
    {
        [Required]
        public string DisplayName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string? IdentityCardFrontSideImageLink { get; set; }
        public string? IdentityCardBackSideImageLink { get; set; }
    }
}
