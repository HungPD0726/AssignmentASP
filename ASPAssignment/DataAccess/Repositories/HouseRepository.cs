using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly FuHouseFinderContext _context;

        public HouseRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public List<House> GetAllHouses()
        {
            // Lấy danh sách nhà, kèm theo thông tin chủ nhà (Landlord)
            // .Include yêu cầu: using Microsoft.EntityFrameworkCore;
            return _context.Houses.ToList();
        }

        public void Add(House house)
        {
            _context.Houses.Add(house);
            _context.SaveChanges();
        }

        public House GetHouseById(int id)
        {
            return _context.Houses.FirstOrDefault(h => h.HouseID == id);
        }

        public void Update(House house)
        {
            _context.Houses.Update(house);
            _context.SaveChanges();
        }

        public void Delete(House house)
        {
            _context.Houses.Remove(house);
            _context.SaveChanges();
        }
    }
}