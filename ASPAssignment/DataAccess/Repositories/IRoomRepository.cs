// ASPAssignment.DataAccess.Repositories/IRoomRepository.cs
using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.DataAccess.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        List<Room> GetRoomsByHouseId(int houseId);
        void Add(Room room);
        void Update(Room room);
        void Delete(Room room);
    }
}