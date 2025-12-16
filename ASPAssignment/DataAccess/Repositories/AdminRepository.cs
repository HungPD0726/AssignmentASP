using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FuHouseFinderContext _context;

        public AdminRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }
    }
}
