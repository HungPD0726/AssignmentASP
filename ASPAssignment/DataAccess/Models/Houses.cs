using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("Houses")]
    public class House
    {
        [Key]
        public int HouseID { get; set; }

        [Required]
        [StringLength(200)]
        public string HouseName { get; set; }

        public string Information { get; set; }

        public decimal PowerPrice { get; set; }
        public decimal WaterPrice { get; set; }

        public int LandlordID { get; set; }
        [ForeignKey("LandlordID")]
        public User Landlord { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}