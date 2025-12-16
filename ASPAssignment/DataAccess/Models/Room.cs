using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAssignment.DataAccess.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        [StringLength(100)]
        public string RoomName { get; set; }

        public decimal Price { get; set; }

        public string Information { get; set; }

        public double Area { get; set; } // Diện tích

        public int MaxAmountOfPeople { get; set; }
        public int CurrentAmountOfPeople { get; set; }

        // Khóa ngoại: Thuộc về House nào
        public int HouseID { get; set; }
        [ForeignKey("HouseID")]
        public House House { get; set; }
    }
}