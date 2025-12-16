using ASPAssignment.Business.DTO;

namespace ASPAssignment.Business
{
    public interface IAuthService
    {
        string Login(LoginModel loginModel);
        bool Register(RegisterModel registerModel);

    }
}