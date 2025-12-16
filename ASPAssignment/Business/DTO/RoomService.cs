// ASPAssignment.Business/RoomService.cs
using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Models;
using ASPAssignment.DataAccess.Repositories;

namespace ASPAssignment.Business
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHouseRepository _houseRepository; // Cần dùng để kiểm tra chủ nhà

        public RoomService(IRoomRepository roomRepository, IHouseRepository houseRepository)
        {
            _roomRepository = roomRepository;
            _houseRepository = houseRepository;
        }

        // Helper để kiểm tra quyền sở hữu nhà (House)
        private bool IsHouseOwnedBy(int houseId, int currentUserId)
        {
            var house = _houseRepository.GetHouseById(houseId);
            return house != null && house.LandlordID == currentUserId;
        }

        // --- READ Operations ---
        public List<RoomViewModel> GetRoomsByHouseId(int houseId)
        {
            var rooms = _roomRepository.GetRoomsByHouseId(houseId);
            return rooms.Select(r => new RoomViewModel
            {
                RoomID = r.RoomID,
                RoomName = r.RoomName,
                Price = r.Price,
                Area = r.Area,
                Information = r.Information,
                MaxAmountOfPeople = r.MaxAmountOfPeople,
                CurrentAmountOfPeople = r.CurrentAmountOfPeople,
                HouseID = r.HouseID,
                HouseName = r.House?.HouseName // Now includes House from Repository
            }).ToList();
        }

        public RoomViewModel GetRoomDetail(int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null) return null;

            return new RoomViewModel
            {
                RoomID = room.RoomID,
                RoomName = room.RoomName,
                Price = room.Price,
                Area = room.Area,
                Information = room.Information,
                MaxAmountOfPeople = room.MaxAmountOfPeople,
                CurrentAmountOfPeople = room.CurrentAmountOfPeople,
                HouseID = room.HouseID,
                HouseName = room.House?.HouseName // Đã include trong Repository
            };
        }

        // --- C/U/D Operations (Requires Authorization) ---

        public (bool success, string message) AddRoom(int houseId, CreateRoomModel model, int currentUserId)
        {
            // 1. Kiểm tra quyền sở hữu nhà
            if (!IsHouseOwnedBy(houseId, currentUserId))
            {
                return (false, "Bạn không có quyền thêm phòng cho ngôi nhà này.");
            }

            // 2. Tạo đối tượng Room mới
            var newRoom = new Room
            {
                RoomName = model.RoomName,
                Price = model.Price,
                Area = model.Area,
                Information = model.Information,
                MaxAmountOfPeople = model.MaxAmountOfPeople,
                CurrentAmountOfPeople = 0, // Mặc định là 0 khi tạo mới
                HouseID = houseId
            };

            // 3. Lưu
            _roomRepository.Add(newRoom);

            return (true, "Thêm phòng thành công.");
        }

        public (bool success, string message) UpdateRoom(int roomId, UpdateRoomModel model, int currentUserId)
        {
            // 1. Tìm Room
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null)
            {
                return (false, "Không tìm thấy phòng.");
            }

            // 2. Kiểm tra quyền sở hữu nhà
            if (!IsHouseOwnedBy(room.HouseID, currentUserId))
            {
                return (false, "Bạn không có quyền sửa phòng này.");
            }

            // 3. Cập nhật thông tin
            room.RoomName = model.RoomName;
            room.Price = model.Price;
            room.Area = model.Area;
            room.Information = model.Information;
            room.MaxAmountOfPeople = model.MaxAmountOfPeople;

            _roomRepository.Update(room);

            return (true, "Cập nhật phòng thành công.");
        }

        public (bool success, string message) DeleteRoom(int roomId, int currentUserId)
        {
            // 1. Tìm Room
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null)
            {
                return (false, "Không tìm thấy phòng.");
            }

            // 2. Kiểm tra quyền sở hữu nhà
            if (!IsHouseOwnedBy(room.HouseID, currentUserId))
            {
                return (false, "Bạn không có quyền xóa phòng này.");
            }

            // 3. Kiểm tra nếu phòng còn người thuê thì không cho xóa
            if (room.CurrentAmountOfPeople > 0)
            {
                return (false, "Không thể xóa phòng đang có người thuê.");
            }

            // 4. Xóa
            _roomRepository.Delete(room);

            return (true, "Xóa phòng thành công.");
        }
    }
}