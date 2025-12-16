using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Models;

namespace ASPAssignment.Business
{
    public interface IHouseService
    {
        List<House> GetAllHouses();
        House GetHouseById(int id);
        void AddHouse(CreateHouseModel model, int landlordId);
        bool UpdateHouse(int houseId, UpdateHouseModel model, int currentUserId);
        bool DeleteHouse(int houseId, int currentUserId);
    }
}