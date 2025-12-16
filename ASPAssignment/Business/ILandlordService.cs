using ASPAssignment.Business.DTO;

namespace ASPAssignment.Business
{
    public interface ILandlordService
    {
        Task<AuthResponseDto> RegisterLandlordAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginLandlordAsync(LoginDto dto);
    }
}
