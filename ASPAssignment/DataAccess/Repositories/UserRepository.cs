using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FuHouseFinderContext _context;

        public UserRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            // Include Role để lấy tên quyền
            return _context.Users.Include(u => u.Role).ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserID == id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == id);
        }
    }
}