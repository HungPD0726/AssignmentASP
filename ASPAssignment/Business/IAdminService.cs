using ASPAssignment.Business.DTO;

namespace ASPAssignment.Business
{
    public interface IAdminService
    {
        Task<AuthResponseDto> LoginAdminAsync(LoginDto dto);
    }
}
