using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Repositories
{
    public interface IHouseRepository
    {
        List<House> GetAllHouses();
        void Add(House house);
        House GetHouseById(int id);
        void Update(House house);
        void Delete(House house);
    }

}