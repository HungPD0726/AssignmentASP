using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories.Interfaces
{
    public interface ILanlordRepository
    {
        Task<Landlord> GetByEmailAsync(string email);
        Task<Landlord> AddAsync(Landlord lanlord);
        Task<Landlord?> GetByIdAsync(int id);
    }
}
