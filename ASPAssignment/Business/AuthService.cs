using ASPAssignment.Business.DTO;
using ASPAssignment.DataAccess.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPAssignment.Business
{
    public class AuthService : IAuthService
    {
        private readonly FuHouseFinderContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(FuHouseFinderContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string Login(LoginModel loginModel)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

            if (user == null)
            {
                return null;
            }

            var role = _context.Roles.FirstOrDefault(r => r.RoleID == user.RoleID);
            string roleName = role != null ? role.RoleName : "Student"; // Mặc định là Student nếu lỗi

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Role, roleName) // Thêm role claim để phân quyền
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60), // Token sống trong 60 phút
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool Register(RegisterModel model)
        {
            // 1. Kiểm tra Email đã tồn tại chưa
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                return false; // Email đã tồn tại
            }

            // 2. Tìm RoleID dựa trên tên Role (Student hoặc Landlord)
            var role = _context.Roles.FirstOrDefault(r => r.RoleName == model.RoleName);
            if (role == null)
            {
                // Nếu chưa có Role này trong DB thì tạo mới luôn (cho tiện)
                role = new DataAccess.Models.Role { RoleName = model.RoleName };
                _context.Roles.Add(role);
                _context.SaveChanges();
            }

            // 3. Tạo User mới
            var newUser = new DataAccess.Models.User
            {
                Email = model.Email,
                Password = model.Password, // Lưu ý: Dự án thật phải mã hóa password (MD5/BCrypt)
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                RoleID = role.RoleID,
                Active = true // Mặc định kích hoạt luôn
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return true;
        }
    }
}