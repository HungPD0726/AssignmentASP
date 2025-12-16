using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddAsync(RefreshToken refreshToken);
        Task RevokedAllTokenAsync(String typeName, int id);
        Task<RefreshToken?> GetByTokenAsync(string token);
    }
}
