using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly FuHouseFinderContext _context;

        public RefreshTokenRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task RevokedAllTokenAsync(string typeName, int id)
        {
            var tokens = await _context.RefreshTokens
                .Where(t => t.NameRole == typeName && t.RequestNameRoleId == id && !t.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
