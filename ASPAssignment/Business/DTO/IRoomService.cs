// ASPAssignment.Business/IRoomService.cs
using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.Business
{
    public interface IRoomService
    {
        // READ
        List<RoomViewModel> GetRoomsByHouseId(int houseId);
        RoomViewModel GetRoomDetail(int roomId);

        // CRUD (Yêu cầu kiểm tra Landlord)
        (bool success, string message) AddRoom(int houseId, CreateRoomModel model, int currentUserId);
        (bool success, string message) UpdateRoom(int roomId, UpdateRoomModel model, int currentUserId);
        (bool success, string message) DeleteRoom(int roomId, int currentUserId);
    }
}