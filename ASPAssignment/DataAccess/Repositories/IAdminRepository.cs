using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> GetByEmailAsync(string email);
        Task<Admin?> GetByIdAsync(int id);
    }
}
