using ASPAssignment.Business;
using ASPAssignment.Business.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get current user's profile
        /// </summary>
        [HttpGet]
        public IActionResult GetProfile()
        {
            // Extract userId from JWT token claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ!" });
            }

            var profile = _userService.GetUserProfile(userId);

            if (profile == null)
            {
                return NotFound(new { Message = "Không tìm thấy thông tin người dùng!" });
            }

            return Ok(profile);
        }

        /// <summary>
        /// Update current user's profile
        /// </summary>
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] UpdateProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Extract userId from JWT token claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ!" });
            }

            var result = _userService.UpdateUserProfile(userId, model);

            if (!result)
            {
                return NotFound(new { Message = "Không tìm thấy thông tin người dùng!" });
            }

            return Ok(new { Message = "Cập nhật thông tin thành công!" });
        }
    }
}
