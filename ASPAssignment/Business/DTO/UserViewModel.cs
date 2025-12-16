namespace ASPAssignment.Business.DTO
{
    // Dùng để hiển thị danh sách
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
    }

    // Dùng để Admin update thông tin
    public class UpdateUserModel
    {
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; } // Admin có quyền khóa tài khoản
    }
}