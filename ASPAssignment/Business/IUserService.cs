using ASPAssignment.Business.DTO;

namespace ASPAssignment.Business
{
    public interface IUserService
    {
        List<UserViewModel> GetAllUsers();
        bool UpdateUser(int id, UpdateUserModel model);
        bool DeleteUser(int id);
        UserProfileViewModel GetUserProfile(int userId);
        bool UpdateUserProfile(int userId, UpdateProfileModel model);
    }
}