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
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var houses = _houseService.GetAllHouses();
            return Ok(houses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var house = _houseService.GetHouseById(id);
            if (house == null)
            {
                return NotFound(new { Message = "Không tìm thấy nhà!" });
            }
            return Ok(house);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateHouseModel model)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdString);

            _houseService.AddHouse(model, userId);

            return Ok(new { Message = "Đăng nhà thành công!" });
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateHouseModel model)
        {
            // Lấy ID người đang đăng nhập
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdString);

            var result = _houseService.UpdateHouse(id, model, userId);

            if (!result)
            {
                return BadRequest(new { Message = "Không tìm thấy nhà hoặc bạn không có quyền sửa nhà này!" });
            }

            return Ok(new { Message = "Cập nhật thành công!" });
        }

        // 2. API Xóa nhà
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdString);

            var result = _houseService.DeleteHouse(id, userId);

            if (!result)
            {
                return BadRequest(new { Message = "Không tìm thấy nhà hoặc bạn không có quyền xóa nhà này!" });
            }

            return Ok(new { Message = "Xóa nhà thành công!" });
        }

    }
}