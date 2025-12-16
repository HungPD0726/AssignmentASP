using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Models;
using ASPAssignment.DataAccess.Repositories;

namespace ASPAssignment.Business
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public List<House> GetAllHouses()
        {
            return _houseRepository.GetAllHouses();
        }

        public House GetHouseById(int id)
        {
            return _houseRepository.GetHouseById(id);
        }

        public void AddHouse(CreateHouseModel model, int landlordId)
        {
            var newHouse = new House
            {
                HouseName = model.HouseName,
                Information = model.Information,
                PowerPrice = model.PowerPrice,
                WaterPrice = model.WaterPrice,
                LandlordID = landlordId 
            };

            // Gọi Repository để lưu (bạn cần thêm hàm Add vào Repository trước nhé)
            _houseRepository.Add(newHouse);
        }

        public bool UpdateHouse(int houseId, UpdateHouseModel model, int currentUserId)
        {
            // 1. Tìm nhà trong DB
            var house = _houseRepository.GetHouseById(houseId);

            // 2. Nếu không có nhà, hoặc người sửa không phải chủ nhà -> Chặn
            if (house == null || house.LandlordID != currentUserId)
            {
                return false;
            }

            // 3. Cập nhật thông tin mới
            house.HouseName = model.HouseName;
            house.Information = model.Information;
            house.PowerPrice = model.PowerPrice;
            house.WaterPrice = model.WaterPrice;

            // 4. Lưu xuống DB
            _houseRepository.Update(house);
            return true;
        }

        public bool DeleteHouse(int houseId, int currentUserId)
        {
            var house = _houseRepository.GetHouseById(houseId);

            // Kiểm tra chủ sở hữu
            if (house == null || house.LandlordID != currentUserId)
            {
                return false;
            }

            _houseRepository.Delete(house);
            return true;
        }

    }
}