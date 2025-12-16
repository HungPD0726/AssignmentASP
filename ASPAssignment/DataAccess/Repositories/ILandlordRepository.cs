using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories
{
    public interface ILandlordRepository
    {
        Task<Landlord> GetByEmailAsync(string email);
        Task<Landlord> AddAsync(Landlord landlord);
        Task<Landlord?> GetByIdAsync(int id);
    }
}
