using ASPAssignment.Business;
using ASPAssignment.Business.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ASPAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = _authService.Login(model);

            if (token == null)
            {
                return Unauthorized("Sai email hoặc mật khẩu!");
            }

            return Ok(new { Token = token });
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var result = _authService.Register(model);

            if (!result)
            {
                return BadRequest(new { Message = "Email đã tồn tại hoặc lỗi hệ thống!" });
            }

            return Ok(new { Message = "Đăng ký thành công!" });
        }
    }
}