using ASPAssignment.Business;
using ASPAssignment.Business.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Chỉ ai có Role là 'Admin' mới được vào tất cả các hàm bên dưới
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // 1. Xem danh sách User (Read)
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        // 2. Cập nhật User (Update - Ví dụ khóa tài khoản)
        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserModel model)
        {
            var result = _userService.UpdateUser(id, model);
            if (!result) return NotFound("Không tìm thấy User");
            return Ok("Cập nhật thành công");
        }

        // 3. Xóa User (Delete)
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (!result) return NotFound("Không tìm thấy User");
            return Ok("Xóa thành công");
        }
    }
}