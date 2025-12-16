namespace ASPAssignment.Business.DTO
{
    public class RoomViewModel
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public decimal Price { get; set; }
        public double Area { get; set; }
        public string Information { get; set; }
        public int MaxAmountOfPeople { get; set; }
        public int CurrentAmountOfPeople { get; set; }

        public int HouseID { get; set; }
        public string HouseName { get; set; }
    }
}