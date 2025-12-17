namespace ASPAssignment.Business.DTO
{
    public class UserProfileViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string IdentityCardFrontSideImageLink { get; set; }
        public string IdentityCardBackSideImageLink { get; set; }
    }
}
