using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Repositories;

namespace ASPAssignment.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserViewModel> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            // Map từ Entity sang ViewModel
            return users.Select(u => new UserViewModel
            {
                UserID = u.UserID,
                Email = u.Email,
                DisplayName = u.DisplayName,
                RoleName = u.Role?.RoleName,
                Active = u.Active
            }).ToList();
        }

        public bool UpdateUser(int id, UpdateUserModel model)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return false;

            user.DisplayName = model.DisplayName;
            user.PhoneNumber = model.PhoneNumber;
            user.Active = model.Active;

            _userRepository.Update(user);
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return false;

            _userRepository.Delete(id);
            return true;
        }
    }
}