using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly FuHouseFinderContext _context;

        public LandlordRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public async Task<Landlord> GetByEmailAsync(string email)
        {
            return await _context.Landlords.FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<Landlord> AddAsync(Landlord landlord)
        {
            _context.Landlords.Add(landlord);
            await _context.SaveChangesAsync();
            return landlord;
        }

        public async Task<Landlord?> GetByIdAsync(int id)
        {
            return await _context.Landlords.FindAsync(id);
        }
    }
}
