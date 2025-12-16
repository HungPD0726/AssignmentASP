using ASPAssignment.Business;
using ASPAssignment.Business.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Lấy danh sách phòng theo HouseID (Public - không cần auth)
        /// </summary>
        [HttpGet("house/{houseId}")]
        public IActionResult GetRoomsByHouseId(int houseId)
        {
            var rooms = _roomService.GetRoomsByHouseId(houseId);
            return Ok(rooms);
        }

        /// <summary>
        /// Lấy chi tiết một phòng (Public - không cần auth)
        /// </summary>
        [HttpGet("{roomId}")]
        public IActionResult GetRoomDetail(int roomId)
        {
            var room = _roomService.GetRoomDetail(roomId);
            if (room == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng." });
            }
            return Ok(room);
        }

        /// <summary>
        /// Thêm phòng mới (Chỉ Landlord sở hữu nhà mới được thêm)
        /// </summary>
        [Authorize(Roles = "Landlord")]
        [HttpPost("house/{houseId}")]
        public IActionResult AddRoom(int houseId, [FromBody] CreateRoomModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy UserID từ JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
            {
                return Unauthorized(new { message = "Không xác định được người dùng." });
            }

            var (success, message) = _roomService.AddRoom(houseId, model, currentUserId);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        /// <summary>
        /// Cập nhật thông tin phòng (Chỉ Landlord sở hữu nhà mới được sửa)
        /// </summary>
        [Authorize(Roles = "Landlord")]
        [HttpPut("{roomId}")]
        public IActionResult UpdateRoom(int roomId, [FromBody] UpdateRoomModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy UserID từ JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
            {
                return Unauthorized(new { message = "Không xác định được người dùng." });
            }

            var (success, message) = _roomService.UpdateRoom(roomId, model, currentUserId);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        /// <summary>
        /// Xóa phòng (Chỉ Landlord sở hữu nhà mới được xóa)
        /// </summary>
        [Authorize(Roles = "Landlord")]
        [HttpDelete("{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            // Lấy UserID từ JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
            {
                return Unauthorized(new { message = "Không xác định được người dùng." });
            }

            var (success, message) = _roomService.DeleteRoom(roomId, currentUserId);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }
    }
}
