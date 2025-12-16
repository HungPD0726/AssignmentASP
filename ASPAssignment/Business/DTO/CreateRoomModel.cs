using System.ComponentModel.DataAnnotations;

namespace ASPAssignment.Business.DTO
{
    public class CreateRoomModel
    {
        [Required(ErrorMessage = "Tên phòng là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên phòng không được vượt quá 100 ký tự.")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "Giá phòng là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phòng phải lớn hơn hoặc bằng 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Diện tích là bắt buộc.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0.")]
        public double Area { get; set; }

        [StringLength(500, ErrorMessage = "Thông tin không được vượt quá 500 ký tự.")]
        public string Information { get; set; }

        [Required(ErrorMessage = "Số lượng người tối đa là bắt buộc.")]
        [Range(1, 100, ErrorMessage = "Số lượng người phải từ 1 đến 100.")]
        public int MaxAmountOfPeople { get; set; }
        // HouseID được truyền qua URL, không cần trong model này.
    }
}