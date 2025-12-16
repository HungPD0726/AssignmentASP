using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.Business
{
    public interface ITokenService
    {
        Task<string> GenerateAccessStudentToken(User user);
        Task<string> GenerateAccessLandlordToken(Landlord landlord);
        Task<string> GenerateAccessAdminToken(Admin admin);
        Task<RefreshToken> GenerateRefreshStudentToken(User user);
        Task<RefreshToken> GenerateRefreshLandlordToken(Landlord landlord);
        Task<RefreshToken> GenerateRefreshAdminToken(Admin admin);
        Task<AuthResponseDto> RefreshAsync(string refreshToken);
    }
}
