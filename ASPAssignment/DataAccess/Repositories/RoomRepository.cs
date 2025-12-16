// ASPAssignment.DataAccess.Repositories/RoomRepository.cs
using ASPAssignment.DataAccess.Context;
using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly FuHouseFinderContext _context;

        public RoomRepository(FuHouseFinderContext context)
        {
            _context = context;
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.Include(r => r.House).ToList();
        }

        public Room GetRoomById(int id)
        {
            // Bao gồm House để kiểm tra LandlordID trong Service Layer
            return _context.Rooms.Include(r => r.House).FirstOrDefault(r => r.RoomID == id);
        }

        public List<Room> GetRoomsByHouseId(int houseId)
        {
            return _context.Rooms.Include(r => r.House).Where(r => r.HouseID == houseId).ToList();
        }

        public void Add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}